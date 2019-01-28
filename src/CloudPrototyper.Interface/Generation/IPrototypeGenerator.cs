using CloudPrototyper.Interface.Constants;

namespace CloudPrototyper.Interface.Generation
{
    /// <summary>
    /// Interface for prototype generator implementations. 
    /// </summary>
    public interface IPrototypeGenerator
    {
        /// <summary>
        /// All text and content files are copied to the destination using their relative path. Any exception ends the process.
        /// </summary>
        /// <param name="application">Represents text and content files of an application.</param>
        /// <param name="configProvider">Provides tool configuration.Used to get output path.</param>
        void Generate(IGenerable application, IConfigProvider configProvider);
    }
}
