using System.Collections.Generic;
using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.Interface.Generation
{
    /// <summary>
    /// Interface implemented by everything that want to be generated.
    /// </summary>
    public interface IGenerable
    {
        /// <summary>
        /// Root folder where it will be generated
        /// </summary>
        string OutputFolderPath { get; set; }

        /// <summary>
        /// Text files generated using T4
        /// </summary>
        List<IGenerableFile> Files { get; set; }

        /// <summary>
        /// Other files
        /// </summary>
        List<ContentInfo> Contents { get; set; }
    }
}
