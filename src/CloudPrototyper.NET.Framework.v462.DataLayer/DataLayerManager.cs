using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.Interface.Prototyper;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators;
using CloudPrototyper.NET.Interface.Constants;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.DataLayer
{
    /// <summary>
    /// Represents data layer of all .NET applications. Used to generated data to entiy storages.
    /// </summary>
    public class DataLayerManager : GeneratorManager
    {
        public ApplicationGenerator DataLayer { get; set; }
        public WindsorContainer Container { get; set; } = new WindsorContainer();
        public DataLayerManager(IConfigProvider configProvider, Prototype prototype) : base("DotNet46", configProvider.GetValue("OutputFolderPath") + "\\" + "DataLayer", configProvider.GetValue("OutputFolderPath") + "\\" + "DataLayer", configProvider, prototype)
        {
            DataLayer = new ApplicationGenerator(configProvider.GetValue("OutputFolderPath") + "\\" + "DataLayer");
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));
         
            RegisterDataLayer(prototype, Container);
            var dataLayerHandlers = Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile));
            List<IGenerableFile> dataLayerFiles = ProjectFactory.ResolveHandlers(dataLayerHandlers, Container, NamingConstants.DataLayerProjectName);
            InitDataLayer(dataLayerFiles, Container);
            RegisterSolutionItems();
            

            var allFiles = ProjectFactory.ResolveHandlers(Container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)), Container);
            DataLayer.Files.AddRange(allFiles);

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

            DataLayer.Contents.AddRange(contents);
            packages = packages.Distinct().ToList();
            ProjectFactory.RegisterSolutionLayer(NamingConstants.DataLayerProjectName, ProjectType.Library, packages, DataLayer.Files, includes, contents, new List<AssemblyBase>(), container);
        }

        public override IGenerable GetGenerable()
        {
            return DataLayer;
        }

        public override IList<Resource> GetRequiredResources()
        {
            return Utils.FindAllInstances<Resource>(Prototype)
                .Where(y => Utils.FindAllInstances<Operation>(Prototype)
                    .SelectMany(x => x.GetReferencedResources()).Select(z => z.Name).Contains(y.Name)).ToList();
        }

        public override void Dispose()
        {
            Container.Dispose();
        }


        private void RegisterDataLayer(Prototype prototype, WindsorContainer container)
        {
            EntityFactory.RegisterEntities(prototype, NamingConstants.DataLayerProjectName, container);
            ResourceFactory.RegisterResources(GetRequiredResources().ToList(), prototype, NamingConstants.DataLayerProjectName, container);
        }
        private void RegisterSolutionItems()
        {
            Container.Register(
                Component.For<SolutionFileGenerator>().ImplementedBy<SolutionFileGenerator>().LifestyleSingleton().DependsOn(Dependency.OnValue("solutionName", "DataLayer"))
            );
        }
    }
}
