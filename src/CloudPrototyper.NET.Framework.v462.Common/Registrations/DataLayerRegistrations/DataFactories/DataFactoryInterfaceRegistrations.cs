using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataLayerRegistrations.DataFactories
{
    /// <summary>
    /// Data factory interface registrations.
    /// </summary>
    public class DataFactoryInterfaceRegistrations : GeneratorDependency<DataFactoryInterfaceGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
            {
                Component.For<EntityInterfaceGenerator>()
                    .ImplementedBy<EntityInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
            };
    }
}
