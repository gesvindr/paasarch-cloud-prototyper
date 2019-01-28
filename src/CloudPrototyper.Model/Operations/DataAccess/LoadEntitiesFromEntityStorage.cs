using System.Collections.Generic;

namespace CloudPrototyper.Model.Operations.DataAccess
{
    /// <summary>
    /// Operation loading entities from storage.
    /// </summary>
    public class LoadEntitiesFromEntityStorage : Operation
    {
        /// <summary>
        /// Entity to be loaded.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Entity set to be loaded from.
        /// </summary>
        public string EntitySetName { get; set; }

        /// <summary>
        /// Entity storage to be loaded from.
        /// </summary>
        public string EntityStorageName { get; set; }

        /// <summary>
        /// Filter specifiing output of query.
        /// </summary>
        public FilterCondition Filter { get; set; }

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
