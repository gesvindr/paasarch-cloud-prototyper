using System.Collections.Generic;
using CloudPrototyper.Model.Operations;

namespace CloudPrototyper.NET.Framework.v462.Computing.Models
{
    /// <summary>
    /// Operation simulation URL call.
    /// </summary>
    public class CallUrlOperation : Operation
    {
        public string Url { get; set; }
        public override List<ResourceReference> GetReferencedResources() => new List<ResourceReference>();

        public override List<string> GetReferencedEntities() => new List<string>();
    }
}
