using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CloudPrototyper.Model;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;

namespace CloudPrototyper.NET.Framework.v462.Common.Factories
{
    /// <summary>
    /// Factory class working with entities.
    /// </summary>
    public class EntityFactory
    {
        /// <summary>
        /// Registers all future entities and related generators into the container.
        /// </summary>
        /// <param name="application">Result application.</param>
        /// <param name="prototype">Prototype model.</param>
        /// <param name="projectName">Name of result project containing this entities, used for namespace and folder.</param>
        /// <param name="container">Container storing all IGenerableFile instances.</param>
        public static void RegisterEntities(Prototype prototype, string projectName, WindsorContainer container)
        {
           
            foreach (var entity in prototype.Entities)
            {
                container.Register(
                    Component.For<EntityGenerator>()
                        .ImplementedBy<EntityGenerator>()
                        .LifestyleSingleton()
                        .DependsOn(Dependency.OnValue("projectName", projectName)).DependsOn(Dependency.OnValue("modelParameters", entity)).Named(entity.Name)

                    );
             
                    container.Register(Component.For<DataFactoryGenerator>()
                          .ImplementedBy<DataFactoryGenerator>()
                          .LifestyleSingleton()
                          .DependsOn(Dependency.OnValue("projectName", projectName))
                          .DependsOn(Dependency.OnValue("entityName", entity.Name))
                          .Named(entity.Name + nameof(DataFactoryGenerator)));
                

            }
        }
    }
}
