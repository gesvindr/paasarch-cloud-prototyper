namespace CloudPrototyper.Interface.Generation.Informations
{
    /// <summary>
    /// General information about non T4 generated resource (binary files mostly).
    /// </summary>
    public class ContentInfo
    {
        /// <summary>
        /// Generator will copy it from InputPath.
        /// </summary>
        public string InputPath { get; set; }

        /// <summary>
        /// Generator will copy it to OutputPath.
        /// </summary>
        public string OutputPath { get; set; }
    }
}
