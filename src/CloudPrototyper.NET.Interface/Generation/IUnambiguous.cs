namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Enables exact identification of generators managing part of model (operations, storages...).
    /// </summary>
    public interface IUnambiguous
    {
        /// <summary>
        /// Unique key for identification purposes.
        /// </summary>
        string Key { get; set; }
    }
}
