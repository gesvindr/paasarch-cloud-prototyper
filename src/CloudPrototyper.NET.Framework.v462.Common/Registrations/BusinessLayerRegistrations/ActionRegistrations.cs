using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.BusinessLayerRegistrations
{
    /// <summary>
    /// ActionGenerator registrations.
    /// </summary>
    public class ActionRegistrations : GeneratorDependency<ActionGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
                   => new List<IRegistration>
                   {
               Component.For<ActionBaseGenerator>()
                    .ImplementedBy<ActionBaseGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
                   };
    }
}
