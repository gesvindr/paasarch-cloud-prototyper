using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CloudPrototyper.Interface;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Operations;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Factories
{
    /// <summary>
    /// Factory registering actions and operations to provided container.
    /// </summary>
    public static class OperationFactory
    {
        /// <summary>
        /// Registers actions and operations to provided container.
        /// </summary>
        /// <param name="projectName">Project name used as namespace and folder.</param>
        /// <param name="actions">List of actions withtin the application.</param>
        /// <param name="container"></param>
        public static void RegisterOperations(string projectName,List<Action> actions, WindsorContainer container)
        {
            
                 string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null) return;
            
            
            foreach (var action in actions)
            {
               
                    container.Register(
                    Component.For<ActionGenerator>()
                        .ImplementedBy<ActionGenerator>()
                        .LifestyleSingleton()
                        .DependsOn(Dependency.OnValue("projectName", projectName))
                        .DependsOn(Dependency.OnValue("modelParameters", action))
                        .Named(action.Name+"ActionGenerator"),
                Component.For<Action>()
                       .Instance(action)
                       .LifestyleSingleton()
                       .DependsOn(Dependency.OnValue("projectName", projectName))
                       .DependsOn(Dependency.OnValue("modelParameters", action))
                       .Named(action.Name + "Action"));


            }
            var operations = Utils.FindAllInstances<Operation>(actions);

            foreach (var operation in operations)
            {
                var operationGenerable = Utils.LoadTypes(path).FirstOrDefault(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                                                  t.BaseType.GetGenericTypeDefinition() ==
                                                                  typeof (Modeled<>)
                                                                  &&
                                                                  t.BaseType.GenericTypeArguments.Contains(
                                                                      operation.GetType()));

                if (operationGenerable != null)
                {
                    container.Register(
                        Component.For<Operation>()
                            .Instance(operation)
                            .LifestyleSingleton()
                            .Named(operation.Name),
                        Component.For<Modelable>()
                            .ImplementedBy(operationGenerable)
                            .LifestyleSingleton()
                            .DependsOn(Dependency.OnValue("projectName", projectName))
                            .DependsOn(Dependency.OnValue("modelParameters", operation))
                            .Named(operation.Name + operationGenerable.Name));
                }
            }
        }
    }
}
