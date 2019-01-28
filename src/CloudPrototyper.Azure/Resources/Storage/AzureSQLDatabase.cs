using CloudPrototyper.Model.Resources.Storage;

namespace CloudPrototyper.Azure.Resources.Storage
{
    /// <summary>
    /// Azure SQL database representation
    /// </summary>
    public class AzureSQLDatabase : RelationalDatabase
    {
        /// <summary>
        /// Performance tier of database, for example "standard","basic"
        /// </summary>
        public string PerformanceTier { get; set; }
        /// <summary>
        /// Service objective, for example "S3", "P1"
        /// </summary>
        public string ServiceObjective { get; set; }
    }
}
