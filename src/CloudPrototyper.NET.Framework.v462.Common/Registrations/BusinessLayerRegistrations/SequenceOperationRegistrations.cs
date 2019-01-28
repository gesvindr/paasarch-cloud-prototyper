using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.BusinessLayerRegistrations
{
    /// <summary>
    /// SequenceOperationGenerator registrations.
    /// </summary>
    public class SequenceOperationRegistrations : GeneratorDependency<SequenceOperationGenerator>
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
