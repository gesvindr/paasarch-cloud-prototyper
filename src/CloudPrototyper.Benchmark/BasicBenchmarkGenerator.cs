using System.IO;
using System.Linq;
using System.Text;
using CloudPrototyper.Interface.Benchmark;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Applications;

namespace CloudPrototyper.Benchmark
{
    /// <summary>
    /// Generates files with the set of URLs.
    /// </summary>
    public class BasicBenchmarkGenerator : IBenchmarkGenerator
    {
        private IConfigProvider _configProvider;
        private Prototype _prototype;
        private string _outputPathFolder;
        
        /// <summary>
        /// Generates files with the set of URLs.
        /// </summary>
        /// <param name="configProvider">Provides tool configuration.</param>
        /// <param name="prototype">Model of prototype with informations about callable actions.</param>
        public void GenerateBenchmark(IConfigProvider configProvider, Prototype prototype)
        {
            _configProvider = configProvider;
            _prototype = prototype;
            _outputPathFolder = _configProvider.GetValue("BenchmarkFileOutput");
            foreach (var api in _prototype.Applications.OfType<RestApiApplication>())
            {
                PrintEndpoints(api);
            }
        }

        private void PrintEndpoints(RestApiApplication apiApplication)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var action in apiApplication.Actions)
            {
                stringBuilder.AppendLine(apiApplication.BaseUrl + "/api/action/" + action.Name);
            }


            Directory.CreateDirectory(_outputPathFolder);
            File.WriteAllText(Path.Combine(_outputPathFolder, apiApplication.Name +".txt"), stringBuilder.ToString());
        }
    }
}
