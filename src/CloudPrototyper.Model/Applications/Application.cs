namespace CloudPrototyper.Model.Applications
{
    /// <summary>
    /// Represents application.
    /// </summary>
    public abstract class Application
    {
        /// <summary>
        /// Unique name-
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Target platform.
        /// </summary>
        public string Platform { get; set; }   

        /// <summary>
        /// Target cloud provider.
        /// </summary>
        public string DeployTo { get; set; }
    }
}
