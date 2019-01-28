using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.AzureSqlDatabase;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Interface.Generation;
using QueryGenerator = CloudPrototyper.NET.Interface.Generation.QueryGenerator;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataLayerRegistrations.AzureSqlDatabase
{
    /// <summary>
    /// Azure sql db registrations.
    /// </summary>
    public class AzureSqlDatabaseContextRegistrations : GeneratorDependency<AzureSqlDatabaseContextGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
           => new List<IRegistration>
       {
               Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<EntityInterfaceGenerator>()
                    .ImplementedBy<EntityInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
           Component.For<QueryGenerator>()
               .ImplementedBy<QueryGenerator>()
               .LifestyleSingleton()
               .DependsOn(Dependency.OnValue("projectName", projectName))
       };
    }
}
