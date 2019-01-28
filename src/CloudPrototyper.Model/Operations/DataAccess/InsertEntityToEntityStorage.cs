using System.Collections.Generic;

namespace CloudPrototyper.Model.Operations.DataAccess
{
    /// <summary>
    /// Inserts entity to storage operation.
    /// </summary>
    public class InsertEntityToEntityStorage : Operation
    {
        /// <summary>
        /// Entity name to be inserted.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Entity set to be inserted to.
        /// </summary>
        public string EntitySetName { get; set; }

        /// <summary>
        /// Entity storage to be inserted to.
        /// </summary>
        public string EntityStorageName { get; set; }

        /// <summary>
        /// Number of entities to be inserted to.
        /// </summary>
        public int NumberOfEntities { get; set; }

        public override List<ResourceReference> GetReferencedResources()
        {
            return new List<ResourceReference>()
            {
                new ResourceReference(typeof(Resources.Storage.EntityStorage), EntityStorageName)
            };
        }

        public override List<string> GetReferencedEntities()
        {
            return new List<string>() { EntityName };
        }
    }
}
