using System.Collections.Generic;
using CloudPrototyper.Model.Operations;

namespace CloudPrototyper.NET.Framework.v462.Computing.Models
{
    /// <summary>
    /// Operation simulating image tresholing.
    /// </summary>
    public class ImageTresholding : Operation
    {
        public override List<ResourceReference> GetReferencedResources() => new List<ResourceReference>();
        public override List<string> GetReferencedEntities() => new List<string>();
    }
}
