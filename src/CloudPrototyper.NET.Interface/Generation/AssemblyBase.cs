using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Base class for .NET project file, constains all informations needed because of AssemblyInfo, also can be generated because of GeneratorBase.
    /// </summary>
    public abstract class AssemblyBase : GeneratorBase, IAssembly
    {
        /// <summary>
        /// Files in project.
        /// </summary>
        public List<IGenerableFile> Files { get; set; }

        /// <summary>
        /// Informations about project.
        /// </summary>
        public AssemblyInfo AssemblyInfo { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assemblyInfo">Informations about project.</param>
        /// <param name="files">Project files.</param>
        /// <param name="generationInfo">.csproj file info for generator.</param>
        protected AssemblyBase(AssemblyInfo assemblyInfo, List<IGenerableFile> files, GenerationInfo generationInfo) : base(generationInfo)
        {
            Files = files;
            AssemblyInfo = assemblyInfo;
        }
    }
}
