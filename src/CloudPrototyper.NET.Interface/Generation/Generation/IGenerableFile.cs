using GenerationInfo = CloudPrototyper.NET.Interface.Generation.Generation.Informations.GenerationInfo;

namespace CloudPrototyper.NET.Interface.Generation.Generation
{
    public interface IGenerableFile
    {
        GenerationInfo GenerationInfo { get; set; }
        string GetFileText();
    }
}
