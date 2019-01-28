using CloudPrototyper.NET.Framework.v462.Common.Templates.BusinessLayerTemplates;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators
{
    /// <summary>
    /// Base class for action classes.
    /// </summary>
    public class ActionBaseGenerator : CodeGeneratorBase
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public ActionBaseGenerator(string projectName, OperationInterfaceGenerator operationInterface) : base(projectName, "Actions", "ActionBase", typeof(ActionBaseTemplate))
        {
            OperationInterface = operationInterface;
        }
    }
}
