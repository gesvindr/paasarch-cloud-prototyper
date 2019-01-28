using System.Collections.Generic;

namespace CloudPrototyper.NET.Interface.Generation.Informations
{
    /// <summary>
    /// Informations required in solutin file.
    /// </summary>
    public class SolutionInfo
    {
        /// <summary>
        /// Name of solution.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// Projects in this solution.
        /// </summary>
        public List<AssemblyBase> Assemblies { get; set; } 
    }
}
