using System.Collections.Generic;
using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.Interface.Generation
{
    /// <summary>
    /// Base class of all text files that are generated using T4.
    /// </summary>
    public abstract class GeneratorBase : IGenerableFile
    {
        /// <summary>
        /// General info about result file.
        /// </summary>
        public GenerationInfo GenerationInfo { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="generationInfo">Info for generation purposes.</param>
        protected GeneratorBase(GenerationInfo generationInfo)
        {
            GenerationInfo = generationInfo;
        }

        /// <summary>
        /// Method used by generator to transform model and template into string, that can be
        /// written into file specified by GenerationInfo property.
        /// </summary>
        /// <returns>Text content of file.</returns>
        public virtual string GetFileText()
        {
            if (GenerationInfo.CanInitialize)
            {
                GenerationInfo.Template.Session = new Dictionary<string, object> { ["Model"] = this };
                GenerationInfo.Template.Initialize();
            }
            return GenerationInfo.Template.TransformText();
        }
    }
}
