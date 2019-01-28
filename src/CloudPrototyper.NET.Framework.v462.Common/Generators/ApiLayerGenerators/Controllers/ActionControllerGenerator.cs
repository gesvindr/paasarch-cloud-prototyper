using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates.Controllers;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Controllers
{
    /// <summary>
    /// Rest Api Controller.
    /// </summary>
    public class ActionControllerGenerator : CodeGeneratorBase
    {
        public ActionGenerator Action { get; set; }
        public CustomHttpActivatorGenerator HttpActivator { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public ActionControllerGenerator(string projectName, string actionName, IList<ActionGenerator> actions, CustomHttpActivatorGenerator httpActivator, StorageInterfaceGenerator storageInterfaceGenerator, OperationInterfaceGenerator operationInterface) : base(projectName, "Controllers", actions.Single(x => x.Key == actionName).Key+"Controller", typeof(ActionControllerTemplate))
        {
            
            OperationInterface = operationInterface;
            Action = actions.Single(x => x.Key == actionName);
            StorageInterface = storageInterfaceGenerator;
            HttpActivator = httpActivator;
        }
    }
}

