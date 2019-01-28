using System.Collections.Generic;
using CloudPrototyper.Interface.Constants;

namespace CloudPrototyper.Interface.Build
{
    /// <summary>
    /// Base class for IBuilder instance.
    /// </summary>
    public abstract class BuilderBase : IBuilder
    {
        /// <summary>
        /// List of platforms supported by this instance.
        /// </summary>
        public List<string> SupportedPlatrofms { get; }

        /// <summary>
        /// Checks if platform is supported by this instance.
        /// </summary>
        /// <param name="platform">Unique platform string identifier.</param>
        /// <returns></returns>
        public bool IsPlatformSupported(string platform) => SupportedPlatrofms.Contains(platform);

        /// <summary>
        /// Proceeds build.
        /// </summary>
        /// <param name="config">Provides configuration.</param>
        /// <param name="buildable">Informations about built application.</param>
        public abstract void Build(IConfigProvider config, IBuildable buildable);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="supportedPlatforms">List of supported platforms of this instance.</param>
        protected BuilderBase(List<string> supportedPlatforms)
        {
            SupportedPlatrofms = supportedPlatforms;
        }
    }
}
