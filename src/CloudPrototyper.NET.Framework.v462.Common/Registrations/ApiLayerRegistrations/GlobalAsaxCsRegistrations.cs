using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.ApiLayerRegistrations
{
    /// <summary>
    /// GlobalAsaxCsGenerator registrations.
    /// </summary>
    public class GlobalAsaxCsRegistrations : GeneratorDependency<GlobalAsaxCsGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
        => new List<IRegistration>
        {
               Component.For<CustomHttpActivatorGenerator>()
                    .ImplementedBy<CustomHttpActivatorGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
               Component.For<WebApiContainerInstallerGenerator>()
                    .ImplementedBy<WebApiContainerInstallerGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
        };
    }
}
