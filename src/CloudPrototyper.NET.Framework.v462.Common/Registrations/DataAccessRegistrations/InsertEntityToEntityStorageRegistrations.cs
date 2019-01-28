using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataAccess;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataAccessRegistrations
{
    /// <summary>
    /// InsertEntityToStorageGenerator registrations.
    /// </summary>
    public class InsertEntityToEntityStorageRegistrations : GeneratorDependency<InsertEntityToEntityStorageGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
        {
               Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
        };
    }
}
