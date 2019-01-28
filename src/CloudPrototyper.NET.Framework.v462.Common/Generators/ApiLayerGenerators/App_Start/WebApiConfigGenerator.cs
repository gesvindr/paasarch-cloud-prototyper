using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.App_Start
{ 
    /// <summary>
    /// WebApi.Config.
    /// </summary>
    public class WebApiConfigGenerator : CodeGeneratorBase
    {
        public WebApiConfigGenerator(string ns, string name, GenerationInfo generationInfo) : base(ns, name, generationInfo)
        {
        }
    }
}
