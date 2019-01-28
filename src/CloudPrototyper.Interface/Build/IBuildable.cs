namespace CloudPrototyper.Interface.Build
{
    /// <summary>
    /// Provides basic build informations about application.
    /// </summary>
    public interface IBuildable 
    {
        /// <summary>
        /// Output folder of build process.
        /// </summary>
        string OutputBuildPath { get; set; }

        /// <summary>
        /// Root folder of application code. Builder shall be able to find what he need to proceed build on his own.
        /// </summary>
        string SolutionRootPath { get; set; }

        /// <summary>
        /// Unique string platform identifies.
        /// </summary>
        string Platform { get; set; }
    }
}
