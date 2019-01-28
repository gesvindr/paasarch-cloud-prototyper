namespace CloudPrototyper.Interface.Generation.Informations
{
    /// <summary>
    /// General info about text file that will be generated using T4.
    /// </summary>
    public class GenerationInfo
    {
        /// <summary>
        /// Name of file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Folder relative to output root folder.
        /// </summary>
        public string RelativePathFolder { get; set; }

        /// <summary>
        /// T4 that uses "this" like model.
        /// </summary>
        public dynamic Template { get; set; }

        /// <summary>
        /// If template uses some Model, it has to be initialized. However if it is only plain text
        /// without model i cannot be. See GetFileText method of GeneratorBase.
        /// </summary>
        public bool CanInitialize { get; set; }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="relativePathFolder">Relative folder path.</param>
        /// <param name="template">T4 template instatnce.</param>
        /// <param name="canInitialize">Can we call Initialize function.</param>
        public GenerationInfo(string fileName, string relativePathFolder, dynamic template, bool canInitialize)
        {
            FileName = fileName;
            RelativePathFolder = relativePathFolder;
            Template = template;
            CanInitialize = canInitialize;
        }
    }
}