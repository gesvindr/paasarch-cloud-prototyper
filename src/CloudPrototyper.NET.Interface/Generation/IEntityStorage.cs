namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Implementing this interface will recognize the generator as EntityStorageImplementation.
    /// </summary>
    public interface IEntityStorage : IUnambiguous
    {
        /// <summary>
        /// Every entity storage must referece StorageInterface in result project in order to work with
        /// operations abstracting them from the concrete storage type.
        /// </summary>
        StorageInterfaceGenerator StorageInterface { get; set; }
    }
}
