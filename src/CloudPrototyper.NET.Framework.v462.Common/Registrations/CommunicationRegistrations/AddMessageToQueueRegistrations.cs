using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.Communication;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.CommunicationRegistrations
{
    /// <summary>
    /// AddMessageToQueOperationGenerator registrations.
    /// </summary>
    public class AddMessageToQueueRegistrations : GeneratorDependency<AddMessageToQueueGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
        {
               Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
        };
    }
}
