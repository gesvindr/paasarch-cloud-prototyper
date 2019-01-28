using CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates.Utils;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils
{
    /// <summary>
    /// Http activator.
    /// </summary>
    public class CustomHttpActivatorGenerator : CodeGeneratorBase
    {
        public CustomHttpActivatorGenerator(string projectName)
            : base(projectName, "Utils", "HttpActivator", typeof (CustomHttpActivatorTemplate))
        {
            
        }
    }
}
