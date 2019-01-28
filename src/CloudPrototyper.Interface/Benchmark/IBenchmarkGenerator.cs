using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Model;

namespace CloudPrototyper.Interface.Benchmark
{
    /// <summary>
    /// Every project that can be benchmarked from via urls needs to have these properties
    /// so benchmark will cal BaseOperationUrl + OperationRelativeUrl
    /// </summary>
    public interface IBenchmarkGenerator
    {
        /// <summary>
        /// Generates files with the set of URLs.
        /// </summary>
        /// <param name="configProvider">Provides tool configuration.</param>
        /// <param name="prototype">Model of prototype with informations about callable actions.</param>
        void GenerateBenchmark(IConfigProvider configProvider, Prototype prototype);
    }
}
