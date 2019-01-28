namespace CloudPrototyper.Model.Resources
{
    /// <summary>
    /// Resource base class.
    /// </summary>
    public abstract class Resource
    {
        /// <summary>
        /// Unique name of resource.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cloud provder name.
        /// </summary>
        public string DeployTo { get; set; }
        protected Resource()
        {

        }

        protected bool Equals(Resource other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Resource) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
