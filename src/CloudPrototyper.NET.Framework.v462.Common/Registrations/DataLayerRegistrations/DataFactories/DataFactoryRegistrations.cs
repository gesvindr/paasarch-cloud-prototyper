using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Interface.Generation;
using Component = Castle.MicroKernel.Registration.Component;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.DataLayerRegistrations.DataFactories
{
    /// <summary>
    /// Data factory generator registrations.
    /// </summary>
    public class DataFactoryRegistrations : GeneratorDependency<DataFactoryGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
        {
            var output = new List<IRegistration>
            {
                Component.For<DataFactoryInterfaceGenerator>()
                    .ImplementedBy<DataFactoryInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                Component.For<DataGeneratorGenerator>()
                    .ImplementedBy<DataGeneratorGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))

            };
            
            return output;
        }
    }
}
