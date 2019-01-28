using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.Workers;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.WorkersRegistrations
{
    /// <summary>
    /// Worker entry point registrations.
    /// </summary>
    public class WorkerMainRegistrations : GeneratorDependency<WorkerMainGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
     => new List<IRegistration>
     {
                Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                 Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                  Component.For<MessageBusHandlerInterfaceGenerator>()
                    .ImplementedBy<MessageBusHandlerInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
     };
    }
}
