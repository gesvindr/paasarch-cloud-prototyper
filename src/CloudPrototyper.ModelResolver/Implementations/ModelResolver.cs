using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CloudPrototyper.DataGenerator;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Benchmark;
using CloudPrototyper.Interface.Build;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Deployment;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Prototyper;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.NET.Framework.v462.DataLayer;

namespace CloudPrototyper.ModelResolver.Implementations
{
    /// <summary>
    /// Implementation of ILifeCycleManager. Managing all modules in order to provide functionality for all 
    /// prototy life cycle steps.
    /// </summary>
    public class ModelResolver : ILifeCycleManager
    {
        private readonly Prototype _prototype;
        private readonly Dictionary<Application, GeneratorManager> _generatorManagers = new Dictionary<Application, GeneratorManager>();
        private readonly Dictionary<Application, IBuilder> _builders = new Dictionary<Application, IBuilder>();
        private readonly List<IDeploymentManager> _deploymentManagers = new List<IDeploymentManager>();
        private readonly List<Application> _deployedApplications = new List<Application>();
        private readonly List<Application> _applicationsToDeploy = new List<Application>();
        private readonly List<Application> _preparedApplications = new List<Application>();
        private readonly List<Application> _applicationsToPrepare = new List<Application>();
        private readonly List<Resource> _resources = new List<Resource>();
        private readonly List<Resource> _resourcesToPrepare = new List<Resource>();
        private readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private IPrototypeGenerator _prototypeGenerator;
        private IBenchmarkGenerator _benchmarkGenerator;
        private IConfigProvider _configProvider;
        private GeneratorManager _dataLayer;
        private IBuilder _dataLayerBuilder;
        private IEntityGenerator _entityGenerator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prototype">Prototype model instance.</param>
        /// <param name="configPath">Configuration file path.</param>
        public ModelResolver(Prototype prototype, string configPath)
        {
            _prototype = prototype;
            LoadConfigProvider(configPath);
            LoadGeneratorManagers();
            LoadBuilders();
            LoadDeployers();
            LoadGenerator();
            LoadBenchmark();

            foreach (var manager in _generatorManagers.Values)
            {
                _resourcesToPrepare.AddRange(manager.GetRequiredResources());
            }
            _applicationsToPrepare.AddRange(prototype.Applications);
            _applicationsToDeploy.AddRange(prototype.Applications);

            _applicationsToPrepare = _applicationsToPrepare.Distinct().ToList();
            _resourcesToPrepare = _resourcesToPrepare.Distinct().ToList();
            _applicationsToDeploy = _applicationsToDeploy.Distinct().ToList();
        }

        /// <summary>
        /// Generates and build prototype in order to verify model.
        /// </summary>
        public void VerifyModel()
        {
            GenerateApplications();
            BuildApplications();

        }

        /// <summary>
        /// Prepares resources in order to be prepared.
        /// </summary>
        public void PrepareResources()
        {
            foreach (var deployer in _deploymentManagers)
            {
                deployer.Setup(_configProvider);
                deployer.PrepareResources(_resourcesToPrepare.Where(x => x.DeployTo == deployer.CloudProviderName).ToList());
                foreach (var resource in deployer.PreparedResources)
                {
                    _resources.Add(resource);
                    _resourcesToPrepare.Remove(resource);
                }

                deployer.PrepareApplications(_applicationsToPrepare);
                foreach (var application in deployer.PreparedApplications)
                {
                    _preparedApplications.Add(application);
                    _applicationsToPrepare.Remove(application);
                }
            }
            if (_resourcesToPrepare.Any() || _applicationsToPrepare.Any())
            {
                throw new ArgumentException("Some resources or applications could not be prepared: "
                    + _resourcesToPrepare.Select(x => x.Name).Aggregate((first, second) => $"{first}, {second}")
                    + _applicationsToPrepare.Select(x => x.Name).Aggregate((first, second) => $"{first}, {second}"));
            }
        }

        /// <summary>
        /// Generates and build prototype with resources informations within.
        /// </summary>
        public void Generate()
        {

            _dataLayer = new DataLayerManager(_configProvider, _prototype);

            Type data =
                Utils.LoadTypes(AppDomain.CurrentDomain.BaseDirectory)
                    .Where(y => !y.IsAbstract && y.GetInterfaces().Contains(typeof(IBuilder)))
                    .FirstOrDefault(type => ((IBuilder)Activator.CreateInstance(type)).IsPlatformSupported(_dataLayer.Platform));
            if (data == null)
            {
                throw new ArgumentException("Data cannot be prepared.");
            }
            _dataLayerBuilder = (IBuilder)Activator.CreateInstance(data);


            _prototypeGenerator.Generate(_dataLayer.GetGenerable(), _configProvider);
            _dataLayerBuilder.Build(_configProvider, _dataLayer);

            var supportedStorages = _entityGenerator.Setup(_prototype, _dataLayer, _configProvider);
            _entityGenerator.Generate();
            _dataLayer.Dispose();
            CleanOutput();

            GenerateApplications();
            BuildApplications();

        }

        /// <summary>
        /// Uploads applications into cloud environment.
        /// </summary>
        public void Deploy()
        {

            foreach (var deployer in _deploymentManagers)
            {
                deployer.DeployApplications(_applicationsToDeploy);
                foreach (var application in deployer.DeployedApplications)
                {
                    _deployedApplications.Add(application);
                    _applicationsToDeploy.Remove(application);
                }
            }
            if (_applicationsToDeploy.Any())
            {
                throw new ArgumentException("Some applications could not be deployed: " +
                                            _applicationsToDeploy.Select(x => x.Name)
                                                .Aggregate((first, second) => $"{first}, {second}"));
            }

        }

        /// <summary>
        /// Generates benchmark file output based on IBenchmarkGenerator implementation.
        /// </summary>
        public void GenerateBenchmark()
        {
            _benchmarkGenerator.GenerateBenchmark(_configProvider, _prototype);
        }

        /// <summary>
        /// Deallocate from cloud, deletes generated source files.
        /// </summary>
        public void CleanUp()
        {
            CleanUpAll();
        }

        private void CleanUpAll()
        {
            foreach (var deployer in _deploymentManagers)
            {
                deployer.Clear();
            }
            CleanOutput();

        }
        private void LoadBenchmark()
        {
            var types = Utils.LoadTypes(_baseDirectory);
            Type benchmarkGeneratorType = types
                       .FirstOrDefault(y => !y.IsAbstract && y.GetInterfaces().Contains(typeof(IBenchmarkGenerator)));
            if (benchmarkGeneratorType == null)
            {
                throw new Exception("No benchmark generator found");
            }
            _benchmarkGenerator = (IBenchmarkGenerator)Activator.CreateInstance(benchmarkGeneratorType);
            _entityGenerator = new CustomDataGenerator();
        }
        private void LoadGenerator()
        {
            var types = Utils.LoadTypes(_baseDirectory);
            Type prototypeGeneratorType = types
                .FirstOrDefault(y => !y.IsAbstract && y.GetInterfaces().Contains(typeof(IPrototypeGenerator)));
            if (prototypeGeneratorType == null)
            {
                throw new Exception("No prototype generator found");
            }
            _prototypeGenerator = (IPrototypeGenerator)Activator.CreateInstance(prototypeGeneratorType);
        }

        private void LoadConfigProvider(string configPath)
        {

            var types = Utils.LoadTypes(_baseDirectory);
            Type prototypeGeneratorType = types
                .FirstOrDefault(y => !y.IsAbstract && y.GetInterfaces().Contains(typeof(IConfigProvider)));
            if (prototypeGeneratorType == null)
            {
                throw new Exception("No prototype generator found");
            }
            _configProvider = (IConfigProvider)Activator.CreateInstance(prototypeGeneratorType, configPath);
        }
        private void CleanOutput()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(_configProvider.GetValue("OutputFolderPath"));
            if (!di.Exists)
            {
                return;
            }
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void BuildApplications()
        {
            foreach (var app in _builders.Keys)
            {
                _builders[app].Build(_configProvider, _generatorManagers[app]);
            }
        }

        private void GenerateApplications()
        {
            _generatorManagers.Clear();
            CleanOutput();
            LoadGeneratorManagers();

            foreach (var manager in _generatorManagers.Values)
            {
                IGenerable generable = manager.GetGenerable();
                _prototypeGenerator.Generate(generable, _configProvider);
                manager.Dispose();
            }

        }

        private void LoadBuilders()
        {
            var types = Utils.LoadTypes(_baseDirectory);
            List<Application> applicationsToBeSatisfied = _prototype.Applications.ToList();
            foreach (var app in _prototype.Applications) // We need to find generator for each app
            {
                Type t =
                    types
                        .Where(y => !y.IsAbstract && y.GetInterfaces().Contains(typeof(IBuilder)))
                        .FirstOrDefault(type => ((IBuilder)Activator.CreateInstance(type)).IsPlatformSupported(app.Platform));
                if (t != null)
                {
                    var bobTheBuilder = (IBuilder)Activator.CreateInstance(t);
                    _builders.Add(app, bobTheBuilder);
                    applicationsToBeSatisfied.Remove(app);
                }
            }
            if (applicationsToBeSatisfied.Count != 0)
            {
                throw new ArgumentException("Cannot find builder for some application");
            }

        }

        private void LoadDeployers()
        {
            var types = Utils.LoadTypes(_baseDirectory).Where(y => !y.IsAbstract && y.GetInterfaces().Contains(typeof(IDeploymentManager)));

            foreach (var deployer in types.Select(type => (IDeploymentManager)Activator.CreateInstance(type)))
            {
                _deploymentManagers.Add(deployer);
            }
        }

        private void LoadGeneratorManagers()
        {
            var types = Utils.LoadTypes(_baseDirectory);
            foreach (var app in _prototype.Applications) // We need to find generator for each app
            {
                Type t = types.First(y => y.BaseType != null && y.BaseType.IsGenericType &&
                                                  y.BaseType.GetGenericTypeDefinition() ==
                                                  typeof(GeneratorManager<>) &&
                                                  y.BaseType.GetGenericArguments().Contains(app.GetType()));
                var generatorManager = (GeneratorManager)Activator.CreateInstance(t, app, _prototype, _configProvider);
                if (generatorManager.Platform.Equals(app.Platform))
                {
                    _generatorManagers.Add(app, generatorManager);
                }
            }
        }
    }
}
