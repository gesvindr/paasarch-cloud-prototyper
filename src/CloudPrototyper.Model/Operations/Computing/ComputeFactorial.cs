using System.Collections.Generic;
namespace CloudPrototyper.Model.Operations.Computing
{
    /// <summary>
    /// Computes factorial of UpTo.
    /// </summary>
    public class ComputeFactorial : Operation
    {
        /// <summary>
        /// Factorial is computed to UpTo.
        /// </summary>
        public int UpTo { get; set; }

        public ComputeFactorial()
        {
        }

        public override List<ResourceReference> GetReferencedResources()
        {
            return new List<ResourceReference>();
        }

        public override List<string> GetReferencedEntities()
        {
            return new List<string>();
        }
    }
}
