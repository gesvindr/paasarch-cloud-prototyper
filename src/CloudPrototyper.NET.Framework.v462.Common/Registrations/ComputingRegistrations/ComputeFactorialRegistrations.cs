using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.Computing;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.ComputingRegistrations
{
    /// <summary>
    /// ComputeFactorialGenerator registrations.
    /// </summary>
    public class ComputeFactorialRegistrations : GeneratorDependency<ComputeFactorialGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
            => new List<IRegistration>
        {
                Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
        };
    }
}
