using System.Collections.Generic;
using System.Configuration;
using CloudPrototyper.Interface.Constants;

namespace CloudPrototyper.ConfigProvider
{
    /// <summary>
    /// Implementation of config provider using .config files as an input.
    /// </summary>
    public class DefaultConfigProvider : IConfigProvider
    {
        /// <summary>
        /// Stores key-value entries. 
        /// </summary>
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public DefaultConfigProvider()
        {
            
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Path to file to be loaded immediately.</param>
        public DefaultConfigProvider(string path)
        {
            LoadConfig(path);
        }
        
        /// <summary>
        /// Loads config from file adding entiries to cache.
        /// </summary>
        /// <param name="path">Path to file.</param>
        public void LoadConfig(string path)
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = path;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            foreach (var key in config.AppSettings.Settings.AllKeys)
            {
                _cache.Add(key,config.AppSettings.Settings[key].Value);
            }
        }

        /// <summary>
        /// Gets the value by the key. May thow an exception.
        /// </summary>
        /// <param name="key">Unique identifier of value.</param>
        /// <returns>Value by the key</returns>
        public string GetValue(string key)
        {
            return _cache[key];
        }

        /// <summary>
        /// Gets the value by the key. Cannot throw any exception.
        /// </summary>
        /// <param name="key">Unique identifier of value.</param>
        /// <returns>Value by the key, null if not present.</returns>
        public string TryGetValue(string key)
        {
            return _cache.ContainsKey(key) ?  _cache[key] : null;
        }
    }
}
