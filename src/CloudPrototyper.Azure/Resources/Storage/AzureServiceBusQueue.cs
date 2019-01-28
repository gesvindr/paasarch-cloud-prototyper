using CloudPrototyper.Model.Resources.Storage;
using Newtonsoft.Json;

namespace CloudPrototyper.Azure.Resources.Storage
{
    /// <summary>
    /// Azure Service Bus representation.
    /// </summary>
    public class AzureServiceBusQueue : Queue
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        [JsonIgnore]
        public string ConnectionString { get; set; } = "";

        public int SizeInGB { get; set; } 
    }
}
