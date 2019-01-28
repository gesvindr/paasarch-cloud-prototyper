namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Storage interface generator to be used in extensions.
    /// </summary>
    public class StorageInterfaceGenerator : CodeGeneratorBase
    {
        public QueryGenerator Query { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Name of project where the result file is placed.</param>
        /// <param name="queryGenerator">Query generator.</param>
        public StorageInterfaceGenerator(string projectName, QueryGenerator queryGenerator) : base(projectName, "Interfaces", "IStorage", typeof(StorageInterfaceTemplate))
        {
            Query = queryGenerator;
        }
    }
}
