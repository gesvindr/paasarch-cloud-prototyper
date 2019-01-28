using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators
{ 
    /// <summary>
    /// Generator for assemblyinfo.cs file.
    /// </summary>
    public class AssemblyInfoModelGenerator : GeneratorBase
    {
        public AssemblyInfo AssemblyInfo { get; set; }

        public AssemblyInfoModelGenerator(GenerationInfo generationInfo, AssemblyInfo assemblyInfo) : base(generationInfo)
        {
            AssemblyInfo = assemblyInfo;
        }
    }
}
