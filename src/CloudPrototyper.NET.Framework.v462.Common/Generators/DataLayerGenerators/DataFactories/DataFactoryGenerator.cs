using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.DataFactories;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories
{
    /// <summary>
    /// Data factory generator. Entities are generated using this class in runtime.
    /// </summary>
    public class DataFactoryGenerator : CodeGeneratorBase
    {
        public EntityGenerator Entity { get; }
        public DataFactoryInterfaceGenerator DataFactoryInterface { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }
        public DataFactoryGenerator(string projectName, string entityName,  DataGeneratorGenerator dataGenerator, DataFactoryInterfaceGenerator dataFactoryInterfaceGenerator, IList<EntityGenerator> entities) : base(projectName, "DataFactories",  entities.Single(x => x.Key == entityName).Name+"DataFactory", typeof(DataFactoryTemplate))
        {
            Entity = entities.Single(x => x.Key == entityName);
            DataFactoryInterface = dataFactoryInterfaceGenerator;
            DataGenerator = dataGenerator;
        }
    }
}
