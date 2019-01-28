using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.DataFactories;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories
{
    /// <summary>
    /// Data factory generator.
    /// </summary>
    public class DataFactoryInterfaceGenerator : CodeGeneratorBase
    {
        public EntityInterfaceGenerator EntityInterface { get; set; }

        public DataFactoryInterfaceGenerator(string projectName, EntityInterfaceGenerator entityInterfaceGenerator)
            : base(projectName, "Interfaces", "IDataFactory", typeof(DataFactoryInterfaceTemplate))
        {
            EntityInterface = entityInterfaceGenerator;
        }
    }
}
