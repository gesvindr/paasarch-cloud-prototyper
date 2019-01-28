using System;
using System.IO;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Generation;

namespace CloudPrototyper.Generator
{
    /// <summary>
    /// The implementation of application IPrototypeGenerator, can be used universally across 
    /// all the platforms to transform IGenerable representing an application into the actual
    /// code files.
    /// </summary>
    public class UniversalGenerator : IPrototypeGenerator
    {
        /// <summary>
        /// All text and content files are copied to the destination using their relative path. Any exception ends the process.
        /// </summary>
        /// <param name="application">Represents text and content files of an application.</param>
        /// <param name="configProvider">Provides tool configuration.Used to get output path.</param>
        public void Generate(IGenerable application, IConfigProvider configProvider)
        {
            try
            {
              foreach (var file in application.Files)
                {
                    try
                    {
                        var dirInfo = Directory.CreateDirectory(Path.Combine(application.OutputFolderPath,
                            file.GenerationInfo.RelativePathFolder));
                        
                        File.WriteAllText(
                            Path.Combine(application.OutputFolderPath, file.GenerationInfo.RelativePathFolder,
                                file.GenerationInfo.FileName), file.GetFileText());
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Given application is not valid: " + file.GenerationInfo.FileName + " " + e.Message);
                    }
                }
                foreach (var content in application.Contents)
                {
                    try
                    {
                        var path = Path.GetDirectoryName(Path.Combine(application.OutputFolderPath, content.OutputPath));
                        if (path != null)
                        {
                            Directory.CreateDirectory(path);
                            File.Copy(content.InputPath, Path.Combine(application.OutputFolderPath, content.OutputPath), true);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Given resource is not valid: " + content.InputPath + " : " + e.Message);
                    }
                }

            }
            catch (Exception e)
            {
                 throw new ArgumentException(e.Message);
            }
        }
    }
}
