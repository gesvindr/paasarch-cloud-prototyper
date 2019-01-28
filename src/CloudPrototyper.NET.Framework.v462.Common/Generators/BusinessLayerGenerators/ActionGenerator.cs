using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Framework.v462.Common.Templates.BusinessLayerTemplates;
using CloudPrototyper.NET.Interface.Prototyper;
using Action = CloudPrototyper.Model.Applications.Action;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators
{
    /// <summary>
    /// Generator for action files.
    /// </summary>
    public class ActionGenerator : Modeled<Action>
    {
        public Modelable Operation;
        public ActionBaseGenerator ActionBase;
       
        public ActionGenerator(string projectName, Action modelParameters, IList<Modelable> operations, ActionBaseGenerator actionBase) : base(projectName, "Actions", modelParameters.Name + "Action", typeof(ActionTemplate), modelParameters, modelParameters.Name)
        {
            ActionBase = actionBase;
            Operation = operations.FirstOrDefault(x => x.Key.Equals(modelParameters.Operation.Name));
            Key = modelParameters.Name;
        }
    }
}
