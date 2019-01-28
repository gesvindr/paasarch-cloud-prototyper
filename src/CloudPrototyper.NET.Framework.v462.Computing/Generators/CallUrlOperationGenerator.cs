using CloudPrototyper.NET.Framework.v462.Computing.Models;
using CloudPrototyper.NET.Framework.v462.Computing.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Computing.Generators
{
    public class CallUrlOperationGenerator : Modeled<CallUrlOperation>, IOperation
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }

        public CallUrlOperationGenerator(string projectName, OperationInterfaceGenerator operationInterface, CallUrlOperation modelParameters, bool canInitialize = true) : base(projectName, "Operations", modelParameters.Name, typeof(CallUrlOperationTemplate), modelParameters, modelParameters.Name, canInitialize)
        {
            OperationInterface = operationInterface;
        }
    }
}
