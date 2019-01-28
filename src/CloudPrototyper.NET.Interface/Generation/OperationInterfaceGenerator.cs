namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Operation interface generator to be used in extension projects.
    /// </summary>
    public class OperationInterfaceGenerator : CodeGeneratorBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Name of project where the result file is placed.</param>
        public OperationInterfaceGenerator(string projectName)
            : base(projectName, "Operations", "IOperation", typeof (OperationInterfaceTemplate))
        {
            
        }
    }
}
