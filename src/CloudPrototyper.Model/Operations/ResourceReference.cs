using System;

namespace CloudPrototyper.Model.Operations
{
    /// <summary>
    /// Representation of resource.
    /// </summary>
    public class ResourceReference
    {
        /// <summary>
        /// Resource representation class type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Resource name.
        /// </summary>
        public string Name { get; set; }

        public ResourceReference()
        {

        }

        public ResourceReference(Type type, string name)
        {
            this.Type = type;
            this.Name = name;
        }
    }
}