using System.Collections.Generic;

namespace CloudPrototyper.Model.Operations
{
    /// <summary>
    /// Operation base class.
    /// </summary>
    public abstract class Operation
    {
        /// <summary>
        /// Unique operation name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional description of operation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Resources referenced by the operation.
        /// </summary>
        /// <returns>Resources referenced by the operation.</returns>
        public abstract List<ResourceReference> GetReferencedResources();

        /// <summary>
        /// Entities referenced by the operation.
        /// </summary>
        /// <returns>Entities referenced by the operation.</returns>
        public abstract List<string> GetReferencedEntities();
    }
}
