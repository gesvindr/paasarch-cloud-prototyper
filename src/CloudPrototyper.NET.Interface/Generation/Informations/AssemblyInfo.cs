using System;
using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation.Informations
{
    /// <summary>
    /// Assembly info constains all information to generate .csproj file and also is used in .sln file.
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// Files to be compiled.
        /// </summary>
        public List<IGenerableFile> FilesToCompile { get; set; }

        /// <summary>
        /// Constents to be included.
        /// </summary>
        public List<ContentInfo> Contents { get; set; }

        /// <summary>
        /// Other assemblies to be referenced.
        /// </summary>
        public List<AssemblyBase> AssemblyImports { get; set; }

        /// <summary>
        /// NuGet packages to be referenced.
        /// </summary>
        public List<PackageConfigInfo> Packages { get; set; }

        /// <summary>
        /// Files to be included.
        /// </summary>
        public List<string> IncludeOnlys { get; set; }

        /// <summary>
        /// Path to .csproj file.
        /// </summary>
        public string ProjectFileRelativePath { get; set; }

        /// <summary>
        /// Name of project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique identifier of project type.
        /// </summary>
        public Guid ProjectType { get; set; }

        /// <summary>
        /// Unique identifier of project instance.
        /// </summary>
        public Guid UniqueProjectId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AssemblyInfo()
        {
            IncludeOnlys = new List<string>();
            FilesToCompile = new List<IGenerableFile>();
            Contents = new List<ContentInfo>();
            AssemblyImports = new List<AssemblyBase>();
            Packages = new List<PackageConfigInfo>();
        }
    }
}
