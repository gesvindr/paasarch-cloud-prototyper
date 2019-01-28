using System.Collections.Generic;
using CloudPrototyper.Interface.Constants;

namespace CloudPrototyper.Interface.Build
{
    /// <summary>
    /// Interface for classes that providers build functionality for supported platforms.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// List of platforms supported by this instance.
        /// </summary>
        List<string> SupportedPlatrofms { get; }

        /// <summary>
        /// Checks if platform is supported by this instance.
        /// </summary>
        /// <param name="platform">Unique platform string identifier.</param>
        /// <returns></returns>
        bool IsPlatformSupported(string platform);

        /// <summary>
        /// Proceeds build.
        /// </summary>
        /// <param name="config">Provides configuration.</param>
        /// <param name="buildable">Informations about built application.</param>
        void Build(IConfigProvider config, IBuildable buildable);
    }
}
