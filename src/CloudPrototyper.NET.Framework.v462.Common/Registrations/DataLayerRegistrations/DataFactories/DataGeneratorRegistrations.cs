using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataLayerRegistrations.DataFactories
{
    /// <summary>
    /// DataGenerator registrations.
    /// </summary>
    public class DataGeneratorRegistrations : GeneratorDependency<DataGeneratorGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
        {
            var output = new List<IRegistration>
            {
                Component.For<EntityInterfaceGenerator>()
                    .ImplementedBy<EntityInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                    Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))};

          
            return output;
        }
    }
}
