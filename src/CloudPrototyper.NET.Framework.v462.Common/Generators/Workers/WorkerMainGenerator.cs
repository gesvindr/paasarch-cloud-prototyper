using CloudPrototyper.NET.Framework.v462.Common.Templates.Workers;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.Workers
{
    /// <summary>
    /// Worker application entry point generator.
    /// </summary>
    public class WorkerMainGenerator : CodeGeneratorBase
    {
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public MessageBusInterfaceGenerator MessageBusInterface { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        
        public WorkerMainGenerator(string projectName, StorageInterfaceGenerator storageInterfaceGenerator, MessageBusInterfaceGenerator messageBusInterface, MessageBusHandlerInterfaceGenerator messageBusHandlerInterface) : base(projectName, "", "Program", typeof(WorkerMainTemplate))
        {
            StorageInterface = storageInterfaceGenerator;
            MessageBusHandlerInterface = messageBusHandlerInterface;
            MessageBusInterface = messageBusInterface;
        }
    }
}
