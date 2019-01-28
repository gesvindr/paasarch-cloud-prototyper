using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CloudPrototyper.Interface;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Resources;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Factories
{
    /// <summary>
    /// Pr
    /// </summary>
    public class ResourceFactory
    {
        /// <summary>
        /// Registers all resources file generators.
        /// </summary>        
        /// <param name="projectName">Name of result project containing this entities, used for namespace and folder.</param>
        /// <param name="resources">Resouces to be registered.</param>  
        /// <param name="prototype">Prototype model.</param>
        /// <param name="container">Container storing all IGenerableFile instances.</param>
        public static void RegisterResources(List<Resource> resources,
            Prototype prototype, string projectName, WindsorContainer container)
        {

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path == null) return;
            foreach (var resource in resources)
            {
                var resourceGenerator = Utils.LoadTypes(path).FirstOrDefault(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                                                  t.BaseType.GetGenericTypeDefinition() ==
                                                                  typeof (Modeled<>)
                                                                  &&
                                                                  t.BaseType.GenericTypeArguments.Contains(
                                                                      resource.GetType()));

                if (resourceGenerator != null)
                {
                    container.Register(
                        Component.For(resource.GetType())
                            .Instance(resource)
                            .LifestyleSingleton()
                            .Named(resource.Name),
                        Component.For<Modelable>()
                            .ImplementedBy(resourceGenerator)
                            .LifestyleSingleton()
                            .DependsOn(Dependency.OnValue("projectName", projectName))
                            .DependsOn(Dependency.OnValue("modelParameters", resource))
                            .Named(resource.Name + resource.Name));
                }
            }
        }
    }
}
