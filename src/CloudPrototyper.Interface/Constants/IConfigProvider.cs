namespace CloudPrototyper.Interface.Constants
{
    /// <summary>
    /// General interface for any form of key-value configuration.
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// Loads config from file.
        /// </summary>
        /// <param name="path">Path to file.</param>
        void LoadConfig(string path);
        /// <summary>
        /// Gets the value by the key. May thow an exception.
        /// </summary>
        /// <param name="key">Unique identifier of value.</param>
        /// <returns>Value by the key</returns>
        string GetValue(string key);

        /// <summary>
        /// Gets the value by the key. Cannot throw any exception.
        /// </summary>
        /// <param name="key">Unique identifier of value.</param>
        /// <returns>Value by the key, null if not present.</returns>
        string TryGetValue(string key);
    }
}
