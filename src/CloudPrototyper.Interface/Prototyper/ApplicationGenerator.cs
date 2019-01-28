using System.Collections.Generic;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.Model.Applications;

namespace CloudPrototyper.Interface.Prototyper
{
    /// <summary>
    /// Base class for prototype.
    /// </summary>
    public class ApplicationGenerator : IGenerable
    {
        /// <summary>
        /// Output folder of application files.
        /// </summary>
        public string OutputFolderPath { get; set; }

        /// <summary>
        /// Future text files.
        /// </summary>
        public List<IGenerableFile> Files { get; set; }

        /// <summary>
        /// Binary files to be copied into the output folder.
        /// </summary>
        public List<ContentInfo> Contents { get; set; }

        /// <summary>
        /// Constrtuctor.
        /// </summary>
        /// <param name="outputFolderPath">Output folder of application files.</param>
        public ApplicationGenerator(string outputFolderPath)
        {

            OutputFolderPath = outputFolderPath;
            Files = new List<IGenerableFile>();
            Contents = new List<ContentInfo>();
        }
    }
    /// <summary>
    /// Base class for prototype where T is abstract model of application.
    /// </summary>
    /// <typeparam name="T">Application generic parameter.</typeparam>
    public class ApplicationGenerator<T> : ApplicationGenerator where T : Application
    {
     
        /// <summary>
        /// Application model instance.
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        /// Constrtuctor.
        /// </summary>
        /// <param name="model">Application model instance.</param>
        /// <param name="outputFolderPath">Output folder of application files.</param>
        public ApplicationGenerator(T model, string outputFolderPath) : base(outputFolderPath)
        {
            Model = model;
        }

        /// <summary>
        /// Constrtuctor.
        /// </summary>
        /// <param name="model">Application model instance.</param>
        /// <param name="outputFolderPath">Output folder of application files.</param>
        public ApplicationGenerator(object model, string outputFolderPath) : this((T) model, outputFolderPath)
        {
        }
    }
}
