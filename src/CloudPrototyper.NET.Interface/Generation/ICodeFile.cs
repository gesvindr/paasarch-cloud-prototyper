namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Makes every "generator"/"template model" in .NET have name
    ///  and namespace so that it can be referenced from other files
    /// </summary>
    public interface ICodeFile
    {
        /// <summary>
        /// Name of file.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Namespace of file.
        /// </summary>
        string Namespace { get; set; }
    }
}
