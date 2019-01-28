using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using ContentInfo = CloudPrototyper.NET.Interface.Generation.Generation.Informations.ContentInfo;

namespace CloudPrototyper.NET.Interface.Generation.Generation
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
