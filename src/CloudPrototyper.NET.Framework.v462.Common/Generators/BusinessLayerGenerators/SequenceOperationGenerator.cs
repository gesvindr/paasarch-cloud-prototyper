using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.NET.Framework.v462.Common.Templates.BusinessLayerTemplates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators
{
    /// <summary>
    /// Generator for sequence operations.
    /// </summary>
    public class SequenceOperationGenerator : Modeled<SequenceOperation>, IOperation
    {
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public List<Modelable> InnerOperations { get; set; }
        public SequenceOperationGenerator(string projectName, SequenceOperation modelParameters,
            OperationInterfaceGenerator operationInterface, IList<Modelable> innerOperations) : base(projectName, "Operations", modelParameters.Name, typeof(SequenceOperationTemplate), modelParameters, modelParameters.Name)
        {
            InnerOperations = innerOperations.Where(x => modelParameters.Operations.Select(y => y.Name).ToList().Contains(x.Key)).ToList();
            Key = modelParameters.Name;

            OperationInterface = operationInterface;
        }

    }
}
