namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Every operation needs to implements IOperation in result solution.
    /// </summary>
    public interface IOperation : IUnambiguous
    {
        /// <summary>
        /// Implementing this interface in result project enables registration of all operation types.
        /// </summary>
        OperationInterfaceGenerator OperationInterface { get; set; }
    }
}
