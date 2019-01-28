using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.AzureMessageQueue;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataLayerRegistrations.AzureServiceBus
{
    /// <summary>
    /// Azure service bus registrations.
    /// </summary>
    public class AzureServiceBusQueueRegistrations : GeneratorDependency<AzureServiceBusQueueGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
        {
               Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<DataGeneratorGenerator>()
                    .ImplementedBy<DataGeneratorGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
        };

    }
}
