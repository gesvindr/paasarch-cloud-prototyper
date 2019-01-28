using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.ApiLayerRegistrations.ControllerRegistrations
{
    /// <summary>
    /// ActionControllerGenerator registrations.
    /// </summary>
    public class ActionControllerRegistrations : GeneratorDependency<ActionGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
           => new List<IRegistration>
           {
               Component.For<CustomHttpActivatorGenerator>()
                    .ImplementedBy<CustomHttpActivatorGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
           };
    }
}
