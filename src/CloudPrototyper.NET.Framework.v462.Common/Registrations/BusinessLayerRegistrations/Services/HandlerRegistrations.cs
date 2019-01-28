using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators.Services;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.BusinessLayerRegistrations.Services
{
    /// <summary>
    /// HandlerGenerator registrations.
    /// </summary>
    public class HandlerRegistrations : GeneratorDependency<HandlerGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
            {
               Component.For<AzureServiceBusQueue>()
                    .ImplementedBy<AzureServiceBusQueue>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<MessageBusHandlerInterfaceGenerator>()
                    .ImplementedBy<MessageBusHandlerInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
            };
    }
}
