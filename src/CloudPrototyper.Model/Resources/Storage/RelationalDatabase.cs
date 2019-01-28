using Newtonsoft.Json;

namespace CloudPrototyper.Model.Resources.Storage
{
    /// <summary>
    /// Relational database representation.
    /// </summary>
    public class RelationalDatabase : EntityStorage
    {
        /// <summary>
        /// Connection string representation. 
        /// </summary>
        [JsonIgnore]
        public string ConnectionString { get; set; } = "";
    }
}
