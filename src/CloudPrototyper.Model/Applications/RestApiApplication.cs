using System.Collections.Generic;
using Newtonsoft.Json;

namespace CloudPrototyper.Model.Applications
{
    /// <summary>
    /// Application having REST API.
    /// </summary>
    public class RestApiApplication : Application
    {
        /// <summary>
        /// Base url of deployed application.
        /// </summary>
        [JsonIgnore]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Actions within application
        /// </summary>
        public List<CallableAction> Actions { get; set; }

        public RestApiApplication()
        {
        }
    }
}
