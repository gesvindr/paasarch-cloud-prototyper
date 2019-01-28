namespace CloudPrototyper.Model.Resources.Storage
{
    public class EntitySet
    {
        /// <summary>
        /// Name of the table generated for this entity set, if not provided EntityName is used
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of entity within.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Count { get; set; }
    }
}