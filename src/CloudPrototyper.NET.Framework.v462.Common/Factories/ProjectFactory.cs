using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.Windsor;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.App_Start;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.AssemblyFiles;
using CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.Packages;
using CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates;
using CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates.App_Start;
using CloudPrototyper.NET.Framework.v462.Common.Templates.SolutionTemplates.Assemblies.NuGets;
using CloudPrototyper.NET.Framework.v462.Common.Templates.SolutionTemplates.Assemblies.Properties;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using Component = Castle.MicroKernel.Registration.Component;


namespace CloudPrototyper.NET.Framework.v462.Common.Factories
{
    /// <summary>
    /// Provides functionality to register project related generators.
    /// </summary>
    public static class ProjectFactory
    {
        /// <summary>
        /// Registers all future entities and related generators into the container.
        /// </summary>
        /// <param name="application">Result application.</param>
        /// <param name="prototype">Prototype model.</param>
        /// <param name="projectName">Name of result project containing this entities, used for namespace and folder.</param>
        /// <param name="container">Container storing all IGenerableFile instances.</param>
        public static void RegisterEntities(string projectName, Application application, Prototype prototype, WindsorContainer container)
        {
            EntityFactory.RegisterEntities(prototype, projectName, container);
        }

        /// <summary>
        /// Registers all resources file generators.
        /// </summary>        
        /// <param name="projectName">Name of result project containing this entities, used for namespace and folder.</param>
        /// <param name="application">Result application.</param>        
        /// <param name="prototype">Prototype model.</param>
        /// <param name="operations">List of operations that may use resources.</param>
        /// <param name="container">Container storing all IGenerableFile instances.</param>
        public static void RegisterResources(string projectName, Application application, Prototype prototype, List<Operation> operations, WindsorContainer container)
        {
            var storages = Utils.FindAllInstances<Resource>(prototype)
                .Where(x => operations.SelectMany(y => y.GetReferencedResources().Select(z => z.Name)).Contains(x.Name)).ToList();

            ResourceFactory.RegisterResources(storages, prototype, projectName, container);
        }

        /// <summary>
        /// Registers actions and operations to provided container.
        /// </summary>
        /// <param name="projectName">Project name used as namespace and folder.</param>
        /// <param name="actions">List of actions withtin the application.</param>
        /// <param name="container"></param>
        public static void RegisterOperations(string projectName, List<Model.Applications.Action> actions, WindsorContainer container)
        {
            OperationFactory.RegisterOperations(projectName, actions, container);
        }

        /// <summary>
        /// Handles library project specific matters.
        /// </summary>
        /// <param name="baseNamespace">Base namespace in project.</param>
        /// <param name="name">Name of project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <returns></returns>
        public static AssemblyBase MakeLibraryProject(string baseNamespace, string name, List<IGenerableFile> includes, List<ContentInfo> contents,
            List<IGenerableFile> files, List<PackageConfigInfo> nugets, List<AssemblyBase> imports)
        {
            AssemblyBase library = new LibraryAssemblyFileGenerator(new List<IGenerableFile>(), new AssemblyInfo
            {
                Name = name,
                ProjectFileRelativePath = name
            });
            library.AssemblyInfo.Contents.AddRange(contents);

            AssemblyInfoModelGenerator assemblyInfo = new AssemblyInfoModelGenerator(new GenerationInfo("AssemblyInfo.cs", Path.Combine(library.GenerationInfo.RelativePathFolder, "Properties"), new AssemblyInfoTemplate(), true), library.AssemblyInfo);
            PackagesConfigGenerator packagesConfig = new PackagesConfigGenerator(nugets, new GenerationInfo("packages.config", library.GenerationInfo.RelativePathFolder, new LibraryPackageConfigTemplate(), true));

            files.Add(library);
            files.Add(packagesConfig);
            files.Add(assemblyInfo);
            

            library.AssemblyInfo.AssemblyImports.AddRange(imports);


            library.AssemblyInfo.FilesToCompile.AddRange(includes);
            library.AssemblyInfo.FilesToCompile.Add(assemblyInfo);

            library.AssemblyInfo.Packages.AddRange(nugets);
            library.AssemblyInfo.IncludeOnlys.Add(packagesConfig.GenerationInfo.FileName);
            library.AssemblyInfo.ProjectType = Guid.Parse("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");
            library.AssemblyInfo.UniqueProjectId = Guid.NewGuid();

            return library;
        }

        /// <summary>
        /// Handles web api project specific matters.
        /// </summary>
        /// <param name="baseNamespace">Base namespace in project.</param>
        /// <param name="name">Name of project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <returns></returns>
        public static AssemblyBase MakeWebApiProject(string baseNamespace, string name, List<IGenerableFile> includes, List<ContentInfo> contents, List<IGenerableFile> files, List<PackageConfigInfo> nugets, List<AssemblyBase> imports)
        {
            AssemblyBase apiProject = new WebApiAssemblyFileGenerator(new List<IGenerableFile>(), new AssemblyInfo { Name = name, ProjectFileRelativePath = name });

            apiProject.AssemblyInfo.ProjectType = Guid.Parse("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");
            apiProject.AssemblyInfo.UniqueProjectId = Guid.NewGuid();
            apiProject.AssemblyInfo.Contents.AddRange(contents);

            AssemblyInfoModelGenerator apiLayerAssemblyInfoModel = new AssemblyInfoModelGenerator(new GenerationInfo("AssemblyInfo.cs", Path.Combine(apiProject.GenerationInfo.RelativePathFolder, "Properties"), new AssemblyInfoTemplate(), true), apiProject.AssemblyInfo);


            GlobalAsaxGenerator globalAsax = new GlobalAsaxGenerator(name, "Global", new GenerationInfo("Global.asax", apiProject.GenerationInfo.RelativePathFolder, new GlobalAsaxTemplate(), true), baseNamespace);
            PackagesConfigGenerator packagesConfig = new PackagesConfigGenerator(nugets, new GenerationInfo("packages.config", apiProject.GenerationInfo.RelativePathFolder, new WebApiPackageConfigTemplate(), true));
            WebConfigGenerator webConfig = new WebConfigGenerator(name, "Web", new GenerationInfo("Web.config", apiProject.GenerationInfo.RelativePathFolder, new WebConfigTemplate(), false));
            WebDebugConfigGenerator webDebugConfig = new WebDebugConfigGenerator(name, "Web.Debug", new GenerationInfo("Web.Debug.config", apiProject.GenerationInfo.RelativePathFolder, new WebDebugConfigTemplate(), false));
            WebReleaseConfigGenerator webReleaseConfig = new WebReleaseConfigGenerator(name, "Web.Release", new GenerationInfo("Web.Release.config", apiProject.GenerationInfo.RelativePathFolder, new WebReleaseConfigTemplate(), false));
            WebApiConfigGenerator webApiConfig = new WebApiConfigGenerator(name, "WebApiConfig", new GenerationInfo("WebApiConfig.cs", Path.Combine(apiProject.GenerationInfo.RelativePathFolder, "App_Start"), new WebApiConfigTemplate(), true));

            files.Add(apiProject);
            files.Add(globalAsax);
            files.Add(packagesConfig);
            files.Add(webConfig);
            files.Add(webDebugConfig);
            files.Add(webReleaseConfig);
            files.Add(webApiConfig);
            files.Add(apiLayerAssemblyInfoModel);

            apiProject.AssemblyInfo.AssemblyImports.AddRange(imports);

            apiProject.AssemblyInfo.Packages.AddRange(nugets);
            apiProject.AssemblyInfo.FilesToCompile.AddRange(includes);
            apiProject.AssemblyInfo.FilesToCompile.Add(apiLayerAssemblyInfoModel);
            apiProject.AssemblyInfo.FilesToCompile.Add(globalAsax);
            apiProject.AssemblyInfo.FilesToCompile.Add(packagesConfig);
            apiProject.AssemblyInfo.FilesToCompile.Add(webConfig);
            apiProject.AssemblyInfo.FilesToCompile.Add(webDebugConfig);
            apiProject.AssemblyInfo.FilesToCompile.Add(webReleaseConfig);
            apiProject.AssemblyInfo.FilesToCompile.Add(webApiConfig);

            return apiProject;
        }

        /// <summary>
        /// Handles console project specific matters.
        /// </summary>
        /// <param name="baseNamespace">Base namespace in project.</param>
        /// <param name="name">Name of project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <returns></returns>
        public static AssemblyBase MakeConsoleProject(string baseNamespace, string name, List<IGenerableFile> includes, List<ContentInfo> contents,
            List<IGenerableFile> files, List<PackageConfigInfo> nugets, List<AssemblyBase> imports)
        {
            AssemblyBase consoleProject = new ConsoleAssemblyFileGenerator(new List<IGenerableFile>(), new AssemblyInfo { Name = name, ProjectFileRelativePath = name });

            consoleProject.AssemblyInfo.ProjectType = Guid.Parse("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");
            consoleProject.AssemblyInfo.UniqueProjectId = Guid.NewGuid();
            PackagesConfigGenerator packagesConfig = new PackagesConfigGenerator(nugets, new GenerationInfo("packages.config", consoleProject.GenerationInfo.RelativePathFolder, new LibraryPackageConfigTemplate(), true));

            AssemblyInfoModelGenerator assemblyInfo = new AssemblyInfoModelGenerator(new GenerationInfo("AssemblyInfo.cs", Path.Combine(consoleProject.GenerationInfo.RelativePathFolder, "Properties"), new AssemblyInfoTemplate(), true), consoleProject.AssemblyInfo);

            files.Add(assemblyInfo);
            files.Add(consoleProject);

            files.Add(packagesConfig);
            consoleProject.AssemblyInfo.Contents.AddRange(contents);

            consoleProject.AssemblyInfo.Packages.AddRange(nugets);
            consoleProject.AssemblyInfo.IncludeOnlys.Add(packagesConfig.GenerationInfo.FileName);
            consoleProject.AssemblyInfo.AssemblyImports.AddRange(imports);
            consoleProject.AssemblyInfo.FilesToCompile.AddRange(includes);
            consoleProject.AssemblyInfo.FilesToCompile.Add(assemblyInfo);

            return consoleProject;
        }


        /// <summary>
        /// Handles solution registrations.
        /// </summary>
        /// <param name="layerName">Base namespace in project.</param>
        /// <param name="includes">Files to be included.</param>
        /// <param name="contents">Content to be included.</param>
        /// <param name="files">All files in project.</param>
        /// <param name="projectType">Type of project.</param>
        /// <param name="nugets">Nugets to be referenced.</param>
        /// <param name="imports">Projects to be referenced.</param>
        /// <param name="container">Container where to register project files.</param>
        public static void RegisterSolutionLayer(string layerName, ProjectType projectType, List<PackageConfigInfo> nugets, List<IGenerableFile> files, List<IGenerableFile> includes, List<ContentInfo> contents,  List<AssemblyBase> imports, WindsorContainer container)
        {
            IAssembly instance;
            switch (projectType)
            {
                case ProjectType.Console:
                    instance = MakeConsoleProject(layerName,
                        layerName, includes, contents, files,
                        nugets,
                        imports);
                    break;

                case ProjectType.Library:
                    instance = MakeLibraryProject(layerName,
                    layerName, includes, contents, files,
                    nugets,
                    imports);
                    break;

                case ProjectType.WebApi:
                    instance = MakeWebApiProject(layerName,
                        layerName, includes, contents, files,
                        nugets,
                        imports);
                    break;

                default:
                    throw new NotSupportedException("This type of project is not supported");
            }
            container.Register(
               Component.For<IAssembly>().Instance(instance
                       ).Named(layerName));

        }

        /// <summary>
        /// Resolvse container handlers into IGenerableFiles
        /// </summary>
        /// <param name="handlers">Handlers to be resolved.</param>
        /// <param name="container">Container containing IGenerableFiles</param>
        /// <param name="projectName">Project name.</param>
        /// <returns>List of resolved IGenerableFiles.</returns>
        public static List<IGenerableFile> ResolveHandlers(IHandler[] handlers, WindsorContainer container, string projectName = null)
        {
            List<IGenerableFile> output = new List<IGenerableFile>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null)
            {
                return output;
            }
            
            List<IHandler> additionalHandlers = container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).ToList();
           
            foreach (var handler in handlers)
            {
                var type = handler.ComponentModel.Implementation;

                var generatorDependenciesType = Utils.LoadTypes(path).FirstOrDefault(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                                                  t.BaseType.GetGenericTypeDefinition() ==
                                                                  typeof(GeneratorDependency<>)
                                                                  &&
                                                                  t.BaseType.GenericTypeArguments.Contains(
                                                                     type));
                if (generatorDependenciesType != null)
                {
                    var generatorDependencies =
                        ((GeneratorDependency)Activator.CreateInstance(generatorDependenciesType)).GetRegistrations(
                            projectName);
                    foreach (var registration in generatorDependencies)
                    {
                        try
                        {

                            container.Register(registration);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }


            }
            foreach (var handler in handlers)
            {
                var file = container.Resolve<IGenerableFile>(handler.ComponentModel.Name);
                output.Add(file);
            }
            additionalHandlers = container.Kernel.GetAssignableHandlers(typeof(IGenerableFile)).ToList().Except(additionalHandlers).ToList();
            foreach (var handler in additionalHandlers)
            {
                var file = container.Resolve<IGenerableFile>(handler.ComponentModel.Name);
                output.Add(file);
            }

            return output;
        }
    }
}
