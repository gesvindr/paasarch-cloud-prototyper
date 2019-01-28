using System.Collections.Generic;
using CloudPrototyper.Interface.Build;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Resources;

namespace CloudPrototyper.NET.Interface.Generation.Generation
{
    /// <summary>
    /// Base class for manager that handles all steps needed in our prototyper. Manager is responsible for
    /// transformation of abstract model into generable projects based on prototype type
    /// </summary>

    public abstract class GeneratorManager : IBuildable
    {
        public abstract IGenerable Generable { get; set; }
        public string OutputBuildPath { get; set; }
        public string SolutionRootPath { get; set; }
        public string Platform { get; set; }
        public abstract IList<Resource> GetRequiredResources();
        protected IConfigProvider ConfigProvider { get; set; }

        protected GeneratorManager(string platform, string outputBuildPath, string solutionRootPath, IConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
            OutputBuildPath = outputBuildPath;
            SolutionRootPath = solutionRootPath;
            Platform = platform;
        }

        public abstract void Clear();
    }

    public abstract class GeneratorManager<T> : GeneratorManager where T : Application
    {
        public Prototype Prototype { get; set; }
        public ApplicationGenerator<T> ApplicationGenerator { get; set; }
        protected GeneratorManager(ApplicationGenerator<T> applicationGenerator, Prototype prototype, string platform, string outputBuildPath, string solutionRootPath, IConfigProvider configProvider)
            : base(platform, outputBuildPath, solutionRootPath, configProvider)
        {
            ApplicationGenerator = applicationGenerator;
            Prototype = prototype;
            Platform = platform;
        }
    }
}
