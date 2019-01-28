using System;
using System.Collections.Generic;

namespace CloudPrototyper.NET.Interface.Generation.Informations
{
    /// <summary>
    /// Represents NuGet packages to be referenced in result projects.
    /// </summary>
    public class PackageConfigInfo
    {
        /// <summary>
        /// Reference strings.
        /// </summary>
        public List<Tuple<string,string>> IncludeHintPathTuples { get;}

        /// <summary>
        /// Unique identifier of package
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Version of package.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Target framework version.
        /// </summary>
        public string Framework { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="includeHintPathTuples">Reference strings.</param>
        /// <param name="id">Unique identifier of package</param>
        /// <param name="version">Version of package.</param>
        /// <param name="framework">Target framework version.</param>
        public PackageConfigInfo(List<Tuple<string, string>> includeHintPathTuples, string id, string version, string framework)
        {
            IncludeHintPathTuples = includeHintPathTuples;
            Id = id;
            Version = version;
            Framework = framework;
        }

        /// <summary>
        /// Packages equals when having same id, version and framework.
        /// </summary>
        /// <param name="other">To compare with.</param>
        /// <returns>True if equals.</returns>
        protected bool Equals(PackageConfigInfo other)
        {
            return string.Equals(Id, other.Id) && string.Equals(Version, other.Version) && string.Equals(Framework, other.Framework);
        }

        /// <summary>
        /// Packages equals when having same id, version and framework.
        /// </summary>
        /// <param name="obj">To compare with.</param>
        /// <returns>True if equals.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PackageConfigInfo) obj);
        }

        /// <summary>
        /// Hashcode from equality members.
        /// </summary>
        /// <returns>Hashcode of instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Version?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Framework?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
