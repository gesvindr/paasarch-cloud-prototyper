using System.Collections.Generic;

namespace CloudPrototyper.Model.Operations.Communication
{
    /// <summary>
    /// Třída reprezentující operaci vložení zprávy do fronty. Jako zpráva se vloží vygenerovaná instance entity daného typu. 
    /// Implementace této operace bude dána typem fronty (Azure Service Bus, Azure Storage Queue).
    /// </summary>
    public class AddMessageToQueue : Operation
    {
        /// <summary>
        /// Name of queue.
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Entity Name to be added to quque.
        /// </summary>
        public string EntityName { get; set; }

        public override List<ResourceReference> GetReferencedResources()
        {
            return new List<ResourceReference>()
            {
                new ResourceReference(typeof(Resources.Storage.Queue), QueueName)
            };
        }

        public override List<string> GetReferencedEntities()
        {
            return new List<string>() { EntityName };
        }
    }
}
