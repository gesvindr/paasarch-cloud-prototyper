using CloudPrototyper.Model.Operations.Computing;
using CloudPrototyper.NET.Framework.v462.Common.Templates.Computing;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.Computing
{
    /// <summary>
    /// Generator of operation computing factorial.
    /// </summary>
    public class ComputeFactorialGenerator : Modeled<ComputeFactorial>, IOperation
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public ComputeFactorialGenerator(string projectName, OperationInterfaceGenerator operationInterface, ComputeFactorial modelParameters) : base(projectName, "Operations", modelParameters.Name, typeof(ComputeFactorialTemplate), modelParameters, modelParameters.Name)
        {
            OperationInterface = operationInterface;
            Key = modelParameters.Name;
        }
    }
}
