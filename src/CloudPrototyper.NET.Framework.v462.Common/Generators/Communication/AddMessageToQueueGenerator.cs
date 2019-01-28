using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Model.Operations.Communication;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.AzureMessageQueue;
using CloudPrototyper.NET.Framework.v462.Common.Templates.Communication;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.Communication
{
    /// <summary>
    ///  Generator for operation adding message to service queue.
    /// </summary>
    public class AddMessageToQueueGenerator : Modeled<AddMessageToQueue>, IOperation
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public MessageBusInterfaceGenerator MessageBusInterface { get; set; }
        public AzureServiceBusQueueGenerator Queue { get; set; }

        public AddMessageToQueueGenerator(string projectName, AddMessageToQueue modelParameters, OperationInterfaceGenerator operationInterface, MessageBusInterfaceGenerator messageBusInterface, IList<Modelable> queue) : base(projectName, "Operations", modelParameters.Name, typeof(AddMessageToQueueTemplate), modelParameters, modelParameters.Name)
        {
            Queue = (AzureServiceBusQueueGenerator)queue.Single(x => x.Key.Equals(modelParameters.QueueName));
            MessageBusInterface = messageBusInterface;
            OperationInterface = operationInterface;
        }
    }
}
