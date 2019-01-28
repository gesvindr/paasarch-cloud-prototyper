using System;
using System.Collections.Generic;
using System.IO;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Base class for all .NET files "generators"/"template models".
    /// </summary>
    public abstract class CodeGeneratorBase : GeneratorBase, ICodeFile
    {
        /// <summary>
        /// Namespace of result code file.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Name of result code file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nugets required by this file.
        /// </summary>
        /// <returns>Nugets required by this file.</returns>
        public virtual List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>();

        /// <summary>
        /// Contents required by this file.
        /// </summary>
        /// <param name="projectName">Project name of the file.</param>
        /// <returns>Contents required by this file.</returns>
        public virtual List<ContentInfo> GetContents(string projectName) => new List<ContentInfo>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ns">Namespace of result code file.</param>
        /// <param name="name">Name of result code file.</param>
        /// <param name="generationInfo">Informations for file generator.</param>
        protected CodeGeneratorBase(string ns, string name, GenerationInfo generationInfo) : base(generationInfo)
        {
            Name = name;
            Namespace = ns;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Project name of file.</param>
        /// <param name="relativeFolder">Relative path in project folder.</param>
        /// <param name="name">Name of file.</param>
        /// <param name="templateType">Type of template.</param>
        /// <param name="canInitialize">If template can be initialized.</param>
        protected CodeGeneratorBase(string projectName, string relativeFolder, string name, Type templateType, bool canInitialize = true) 
            : base(new GenerationInfo(name+".cs", Path.Combine(projectName,relativeFolder), Activator.CreateInstance(templateType), canInitialize))
        {
            Name = name;
            Namespace = (projectName + "." + relativeFolder.Replace('\\', '.')).TrimEnd('.');
        }
    }
}
