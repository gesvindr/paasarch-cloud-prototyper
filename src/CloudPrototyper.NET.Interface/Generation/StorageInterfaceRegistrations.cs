using System.Collections.Generic;
using Castle.MicroKernel.Registration;

namespace CloudPrototyper.NET.Interface.Generation
{
    public class StorageInterfaceRegistrations : GeneratorDependency<StorageInterfaceGenerator>
    {

        public override List<IRegistration> GetRegistrations(string projectName)
        {
            var output = new List<IRegistration>
            {
                Component.For<QueryGenerator>()
                    .ImplementedBy<QueryGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))

            };

            return output;
        }
    }
}