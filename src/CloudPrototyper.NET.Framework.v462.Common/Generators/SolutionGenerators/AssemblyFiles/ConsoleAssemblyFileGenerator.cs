using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Framework.v462.Common.Templates.SolutionTemplates.Assemblies;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators.AssemblyFiles
{
    /// <summary>
    /// Generator for csproj files of console projects.
    /// </summary>
    public class ConsoleAssemblyFileGenerator : AssemblyBase
    {
        public ConsoleAssemblyFileGenerator(List<IGenerableFile> files, AssemblyInfo assemblyInfo) : base(assemblyInfo, files, new GenerationInfo(assemblyInfo.Name + ".csproj", assemblyInfo.ProjectFileRelativePath, new ConsoleAssemblyFileTemplate(), true))
        {
        }
    }
}
