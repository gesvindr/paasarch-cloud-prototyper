using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils;
using CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators
{
    /// <summary>
    /// Global.asax.cs
    /// </summary>
    public class GlobalAsaxCsGenerator : CodeGeneratorBase
    {
        public CustomHttpActivatorGenerator HttpActivator { get; set; }
        public WebApiContainerInstallerGenerator Installer { get; set; }
       
        public GlobalAsaxCsGenerator(string projectName, CustomHttpActivatorGenerator httpActivator, WebApiContainerInstallerGenerator installer) : base(projectName, "", "Global.asax", typeof(GlobalAsaxCsTemplate))
        {
            Installer = installer;
            HttpActivator = httpActivator;
        }
    }
}
