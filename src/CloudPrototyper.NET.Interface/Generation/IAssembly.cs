using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation
{
    /// <summary>
    /// Makes every assembly/project have AssemblyInfo.
    /// </summary>
    public interface IAssembly 
    {
        /// <summary>
        /// Constains information required in .csproject file.
        /// </summary>
        AssemblyInfo AssemblyInfo { get; set; }
    }
}
