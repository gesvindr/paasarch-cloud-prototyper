using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.Packages
{
    public class PackagesConfigGenerator : GeneratorBase
    {
        public List<PackageConfigInfo> Packages { get; set; }  
        public PackagesConfigGenerator(List<PackageConfigInfo> packageConfigInfos, GenerationInfo generationInfo) : base(generationInfo)
        {
            Packages = packageConfigInfos;
        }
    }
}
