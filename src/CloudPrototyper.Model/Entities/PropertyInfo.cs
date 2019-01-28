namespace CloudPrototyper.Model.Entities
{
    /// <summary>
    /// Informations about property. Persisted entites consists of list of this.
    /// </summary>
    public class PropertyInfo
    {
        /// <summary>
        /// Name of property 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Full!!! type name like a.b.c.d
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Used while generating data. If this is reference there will be some 
        /// kind of relation.
        /// </summary>
        public bool IsReference { get; set; }

        /// <summary>
        /// Determined if this is List or not. It helps to model relation between entities.
        /// </summary>
        public bool IsMany { get; set; }

        /// <summary>
        /// Used while generating data. For example is ContentSize == 7, then some string 
        /// might be generated like "trololo", int 1234567...
        /// </summary>
        public int ContentSize { get; set; }

        /// <summary>
        /// Make index on this column?
        /// </summary>
        public bool IsIndexed { get; set; }
    }
}
