namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Message bus interface generator.
    /// </summary>
    public class MessageBusInterfaceGenerator : CodeGeneratorBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="projectName">Name of project where the result file is placed.</param>
        public MessageBusInterfaceGenerator(string projectName) : base(projectName, "Interfaces", "IMessageBus", typeof(MessageBusInterfaceTemplate))
        {
        }
    }
}
