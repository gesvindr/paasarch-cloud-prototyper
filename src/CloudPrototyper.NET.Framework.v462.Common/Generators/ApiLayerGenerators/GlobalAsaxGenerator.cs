using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators
{
    /// <summary>
    /// Global.asax.
    /// </summary>
    public class GlobalAsaxGenerator : CodeGeneratorBase
    {
        public string ProjectName { get; set; }
        public GlobalAsaxGenerator(string ns, string name, GenerationInfo generationInfo, string projectName) : base(ns, name, generationInfo)
        {
            ProjectName = projectName;
        }

    }
}
