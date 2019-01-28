using System;
using System.Collections.Generic;
using CloudPrototyper.Interface.Build;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.Interface.Prototyper
{
    /// <summary>
    /// Base class for manager that handles all steps needed in our prototyper. Manager is responsible for
    /// transformation of abstract model into generable projects based on prototype type.
    /// </summary>
    public abstract class GeneratorManager : IBuildable, IDisposable
    {
        /// <summary>
        /// Model of whole prototype, required for resources and entitites.
        /// </summary>
        public Prototype Prototype { get; set; }
        /// <summary>
        /// Application files.
        /// </summary>
        /// <returns>Application files to be generated.</returns>
        public abstract IGenerable GetGenerable();

        /// <summary>
        /// Output path, part of IBuildable.
        /// </summary>
        public string OutputBuildPath { get; set; }

        /// <summary>
        /// Root folder of generated appplication, part of IBuildable.
        /// </summary>
        public string SolutionRootPath { get; set; }

        /// <summary>
        /// Unique string identifier.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Lists of resources required by managed application.
        /// </summary>
        /// <returns>List of resources required by managed application.</returns>
        public abstract IList<Resource> GetRequiredResources();

        /// <summary>
        /// Provides configuration delivered by user.
        /// </summary>
        protected IConfigProvider ConfigProvider { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prototype">Prototype model.</param>
        /// <param name="platform">Unique string identifier.</param>
        /// <param name="outputBuildPath">Output path, part of IBuildable.</param>
        /// <param name="solutionRootPath">Root folder of generated appplication, part of IBuildable.</param>
        /// <param name="configProvider">Configuration provider used to get users settings.</param>
        protected GeneratorManager(string platform, string outputBuildPath, string solutionRootPath, IConfigProvider configProvider, Prototype prototype)
        {
            ConfigProvider = configProvider;
            OutputBuildPath = outputBuildPath;
            SolutionRootPath = solutionRootPath;
            Platform = platform;
            Prototype = prototype;
        }
        public abstract void Dispose();
    }

    /// <summary>
    /// Generic extension of Generator manager.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GeneratorManager<T> : GeneratorManager where T : Application
    {
        /// <summary>
        /// IGenerable of T application type.
        /// </summary>
        public ApplicationGenerator<T> ApplicationGenerator { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationGenerator">Application generator</param>
        /// <param name="prototype">Prototype model.</param>
        /// <param name="platform">Unique string identifier.</param>
        /// <param name="outputBuildPath">Output path, part of IBuildable.</param>
        /// <param name="solutionRootPath">Root folder of generated appplication, part of IBuildable.</param>
        /// <param name="configProvider">Configuration provider used to get users settings.</param>
        protected GeneratorManager(ApplicationGenerator<T> applicationGenerator, Prototype prototype, string platform, string outputBuildPath, string solutionRootPath, IConfigProvider configProvider)
            : base(platform, outputBuildPath, solutionRootPath, configProvider, prototype)
        {
            ApplicationGenerator = applicationGenerator;
            Platform = platform;
        }
    }
}
