using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Computing.Generators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Computing.Registrations
{
    /// <summary>
    /// Image tresholding operation registrations.
    /// </summary>
    public class ImageThresholdingRegistrations : GeneratorDependency<ImageTresholdingGenerator>
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
