using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Framework.v462.Common.Templates.SolutionTemplates;
using CloudPrototyper.NET.Interface.Constants;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.SolutionGenerators
{
    /// <summary>
    /// Geneator for .sln file of whole .NET solution.
    /// </summary>
    public class SolutionFileGenerator : GeneratorBase
    {
        public List<IAssembly> Assemblies { get; set; }

        public SolutionFileGenerator(string solutionName, IList<IAssembly> assemblies) : base(new GenerationInfo(solutionName+NamingConstants.SolutionFileSuffix, "", new SolutionFileTemplate(), true))
        {
            Assemblies = assemblies.ToList();
        }
    }
}
