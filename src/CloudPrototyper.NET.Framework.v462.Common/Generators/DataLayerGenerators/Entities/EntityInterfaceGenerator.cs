using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.Entities;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities
{
    /// <summary>
    /// Generator for interface implemented by all entities.
    /// </summary>
    public class EntityInterfaceGenerator : CodeGeneratorBase
    {
        public EntityInterfaceGenerator(string projectName) : base(projectName, "Interfaces", "IEntity", typeof(EntityInterfaceTemplate))
        {
        }
    }
}
