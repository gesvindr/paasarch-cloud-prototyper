using System.Collections.Generic;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Interface.Generation.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation.Generation
{
    /// <summary>
    /// Base class for prototype where T is abstract model of application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationGenerator<T> : IGenerable where T : Application
    {
        public string OutputFolderPath { get; set; }
        public List<IGenerableFile> Files { get; set; }
        public List<ContentInfo> Contents { get; set; }
        public T Model { get; set; }

        public ApplicationGenerator(T model, string outputFolderPath)
        {
            Model = model;
            Files = new List<IGenerableFile>();
            Contents = new List<ContentInfo>();
            OutputFolderPath = outputFolderPath;
        }

        public ApplicationGenerator(object model, string outputFolderPath) : this((T) model, outputFolderPath)
        {
        }
    }
}
