using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CloudPrototyper.Azure.Resources;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.Interface.Prototyper;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Controllers;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators;
using CloudPrototyper.NET.Interface.Constants;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using Action = CloudPrototyper.Model.Applications.Action;

namespace CloudPrototyper.NET.Framework.v462.WebApi
{
    /// <summary>
    /// Manages RestApiApplicationType for .NET framework 4.6.2 platform.
    /// </summary>
    public class WebApiManager : GeneratorManager<RestApiApplication>
    {
        /// <summary>
        /// Used to register and resolve all IGenerableFiles.
        /// </summary>
        public WindsorContainer Container { get; set; } = new WindsorContainer();

        /// <summary>
        /// Application files.
        /// </summary>
        /// <returns>Application files to be generated.</returns>
        public override IGenerable GetGenerable()
        {
            return ApplicationGenerator;
        }

        /// <summary>
        /// Lists of resources required by managed application.
        /// </summary>
        /// <returns>List of resources required by managed application.</returns>
        public override IList<Resource> GetRequiredResources()
        {
            var res = Utils.FindAllInstances<Resource>(Prototype)
                    .Where(y => Utils.FindAllInstances<Operation>(ApplicationGenerator.Model)
                        .SelectMany(x => x.GetReferencedResources()).Select(z => z.Name).Contains(y.Name)).ToList();
            res.Add(Utils.FindAllInstances<AzureAppService>(Prototype).Single(x=>x.WithApplication == ApplicationGenerator.Model.Name));
            return res;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="application">Application to be managed.</param>
        /// <param name="prototype">Whole prototype with entities and resources.</param>
        /// <param name="configProvider">Configuration provider.</param>
        public WebApiManager(RestApiApplication application, Prototype prototype, IConfigProvider configProvider) : base(new ApplicationGenerator<RestApiApplication>(application, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name), prototype, "DotNet46", configProvider.GetValue("OutputFolderPath") + "\\" + application.Name, configProvider.GetValue("OutputFolderPath") + "\\" + application.Name
            , configProvider)
        {
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));

            var operations = Utils.FindAllInstances<Operation>(application);
            var actions = Utils.FindAllInstances<Action>(application);


            RegisterDataLayer(application, prototype, operations, Container);
            var dataLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile));

            RegisterBusinessLayer(actions, Container);
            var businessLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).Except(dataLayerHandlers).ToArray();

            RegisterApiLayer(actions);
            var apiLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).Except(dataLayerHandlers).Except(businessLayerHandlers).ToArray();

            RegisterSolutionItems(application);

            List<IGenerableFile> dataLayerFiles = ProjectFactory.ResolveHandlers(dataLayerHandlers, Container, NamingConstants.DataLayerProjectName);
            List<IGenerableFile> businessLayerFiles = ProjectFactory.ResolveHandlers(businessLayerHandlers, Container, NamingConstants.BusinessLayerProjectName);
            List<IGenerableFile> apiLayerFiles = ProjectFactory.ResolveHandlers(apiLayerHandlers, Container, NamingConstants.ApiLayerProjectName);

            InitDataLayer(dataLayerFiles, Container);
            InitBusinessLayer(businessLayerFiles, Container);
            InitApiLayer(apiLayerFiles, Container);

            var allFiles = ProjectFactory.ResolveHandlers(Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)), Container);
            ApplicationGenerator.Files.AddRange(allFiles);
        }

        private void RegisterBusinessLayer(List<Action> actions , WindsorContainer container)
        {
            ProjectFactory.RegisterOperations(NamingConstants.BusinessLayerProjectName, actions, container);
        }

        private void RegisterDataLayer(RestApiApplication application, Prototype prototype, List<Operation> operations, WindsorContainer container)
        {
            ProjectFactory.RegisterEntities(NamingConstants.DataLayerProjectName, application, prototype, container);
            ProjectFactory.RegisterResources(NamingConstants.DataLayerProjectName, application, prototype, operations, container);
        }

        private void RegisterApiLayer(List<Action> actions)
        {
            
            Container.Register(
                    Component.For<GlobalAsaxCsGenerator>().ImplementedBy<GlobalAsaxCsGenerator>().DependsOn(Dependency.OnValue("projectName",NamingConstants.ApiLayerProjectName)).LifestyleSingleton(),
                    Component.For<CustomHttpActivatorGenerator>().ImplementedBy<CustomHttpActivatorGenerator>().DependsOn(Dependency.OnValue("projectName", NamingConstants.ApiLayerProjectName)),
                    Component.For<WebApiContainerInstallerGenerator>().ImplementedBy<WebApiContainerInstallerGenerator>().DependsOn(Dependency.OnValue("projectName", NamingConstants.ApiLayerProjectName))
                    );

            foreach (var action in actions)
            {
                Container.Register(Component.For<ActionControllerGenerator>()
                    .ImplementedBy<ActionControllerGenerator>()
                    .DependsOn(Dependency.OnValue("projectName", NamingConstants.ApiLayerProjectName))
                    .DependsOn(Dependency.OnValue("actionName", action.Name))
                    .LifestyleSingleton()
                    .Named(action.Name + typeof (ActionControllerGenerator)));
            }
        }

        private void RegisterSolutionItems(RestApiApplication application)
        {
            Container.Register(
              Component.For<RestApiApplication>().Instance(application)
              );

            foreach (var action in application.Actions)
            {
                Container.Register(
                    Component.For<CallableAction>().Instance(action).Named(action.Name + nameof(CallableAction))
                    );
            }
            Container.Register(
                    Component.For<SolutionFileGenerator>().ImplementedBy<SolutionFileGenerator>().LifestyleSingleton().DependsOn(Dependency.OnValue("solutionName", application.Name))
                );
        }


        private void InitApiLayer(List<IGenerableFile> includes, WindsorContainer container)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }

            var contents = new List<ContentInfo>();
            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.ApiLayerProjectName));
            }

            ApplicationGenerator.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.ApiLayerProjectName, ProjectType.WebApi, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase> { container.Resolve<AssemblyBase>(NamingConstants.DataLayerProjectName), container.Resolve<AssemblyBase>(NamingConstants.BusinessLayerProjectName) }, container);
        }

        private void InitBusinessLayer(List<IGenerableFile> includes, WindsorContainer container)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }

            var contents = new List<ContentInfo>();
            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.BusinessLayerProjectName));
            }

            ApplicationGenerator.Contents.AddRange(contents);

            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.BusinessLayerProjectName, ProjectType.Library, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase>
            { container.Resolve<AssemblyBase>(NamingConstants.DataLayerProjectName) }, container);
        }

        private void InitDataLayer(List<IGenerableFile> includes, WindsorContainer container)
        {
            var packages = new List<PackageConfigInfo>();

            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                packages.AddRange(include.GetNugetPackages());
            }


            var contents = new List<ContentInfo>();
            foreach (CodeGeneratorBase include in includes.OfType<CodeGeneratorBase>())
            {
                contents.AddRange(include.GetContents(NamingConstants.DataLayerProjectName));
            }

            ApplicationGenerator.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.DataLayerProjectName, ProjectType.Library, packages, ApplicationGenerator.Files, includes, contents, new List<AssemblyBase>(), container);
        }

        public override void Dispose()
        {
            Container?.Dispose();
        }
    }
}

