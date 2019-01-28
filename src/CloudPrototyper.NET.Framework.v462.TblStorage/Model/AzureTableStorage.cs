using CloudPrototyper.Model.Resources.Storage;
using Newtonsoft.Json;

namespace CloudPrototyper.NET.Framework.v462.TblStorage.Model
{
    /// <summary>
    /// Azure Table Storage entity storage type.
    /// </summary>
    public class AzureTableStorage : KeyValueDatabase
    {
        [JsonIgnore]
        public string ConnectionString { get; set; } = "";
    }
}
