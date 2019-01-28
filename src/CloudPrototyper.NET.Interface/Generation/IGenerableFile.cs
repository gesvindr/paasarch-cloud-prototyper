using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation
{
    public interface IGenerableFile
    {
        GenerationInfo GenerationInfo { get; set; }
        string GetFileText();
    }
}
