namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Message bus handler interface generator.
    /// </summary>
    public class MessageBusHandlerInterfaceGenerator : CodeGeneratorBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Name of project where the result file is placed.</param>
        public MessageBusHandlerInterfaceGenerator(string projectName) 
            : base(projectName, "Interfaces", "IHandler",typeof(MessageBusHandlerInterfaceTemplate))
        {
           
        }
    }
}