using System.Collections.Generic;

namespace CloudPrototyper.Model.Resources.Storage
{
    /// <summary>
    /// Resource representing storages.
    /// </summary>
    public abstract class EntityStorage : Resource
    {
        /// <summary>
        /// Set of tables.
        /// </summary>
        public List<EntitySet> EntitySets { get; set; }

        protected EntityStorage()
        {

        }
    }
}
