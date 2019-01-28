using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.Interface.Generation
{
    /// <summary>
    /// Represents on future text file generated using T4 template.
    /// </summary>
    public interface IGenerableFile
    {
        /// <summary>
        /// Informations for generation process and T4 template instance.
        /// </summary>
        GenerationInfo GenerationInfo { get; set; }

        /// <summary>
        /// Transtforms template into the text.
        /// </summary>
        /// <returns>String representation of file content.</returns>
        string GetFileText();
    }
}
