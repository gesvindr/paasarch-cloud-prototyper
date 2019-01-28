using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Computing.Generators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Computing.Registrations
{
    /// <summary>
    /// Call url operation registrations.
    /// </summary>
    public class CallUrlOperationRegistrations : GeneratorDependency<CallUrlOperationGenerator>
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
