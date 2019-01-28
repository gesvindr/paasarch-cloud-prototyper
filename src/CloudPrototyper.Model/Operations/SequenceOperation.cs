using System.Linq;
using System.Collections.Generic;

namespace CloudPrototyper.Model.Operations
{
    /// <summary>
    /// Operation having list of operations within.
    /// </summary>
    public class SequenceOperation : Operation
    {
        /// <summary>
        /// List of inner operations.
        /// </summary>
        public List<Operation> Operations { get; set; }

        /// <summary>
        /// All operations will be repeated this many times
        /// </summary>
        public int NumberOfRepetitions { get; set; } = 1;

        public override List<string> GetReferencedEntities()
        {
            if (Operations == null)
            {
                return new List<string>();
            }

            List<string> referencedEntities = new List<string>();
            foreach (var operation in Operations)
            {
                referencedEntities.AddRange(operation.GetReferencedEntities());
            }

            return referencedEntities.Distinct().ToList();
        }

        public override List<ResourceReference> GetReferencedResources()
        {
            return new List<ResourceReference>();
        }
    }
}
