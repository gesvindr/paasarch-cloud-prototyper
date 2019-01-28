using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataLayerRegistrations.Entities
{
    /// <summary>
    /// Entity registrations.
    /// </summary>
    public class EntityRegistrations : GeneratorDependency<EntityGenerator>
    { 
        public override List<IRegistration> GetRegistrations(string projectName)
       => new List<IRegistration>
       {
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
