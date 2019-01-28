using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.DataFactories;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories
{
    /// <summary>
    /// Data generator generator.
    /// </summary>
    public class DataGeneratorGenerator : CodeGeneratorBase 
    {
        public EntityInterfaceGenerator EntityInterface { get; set; }
        public DataFactoryInterfaceGenerator DataFactoryInterface { get; set; }
        public List<EntityGenerator> Entities { get; set; } 
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public DataGeneratorGenerator(string projectName, EntityInterfaceGenerator entityInterface, DataFactoryInterfaceGenerator dataFactoryInterface, IList<EntityGenerator> entities, StorageInterfaceGenerator storageInterface) : base(projectName, "DataGenerators", "DataGenerator", typeof(DataGeneratorTemplate))
        {
            StorageInterface = storageInterface;
            EntityInterface = entityInterface;
            DataFactoryInterface = dataFactoryInterface;
            Entities = entities.ToList();
        }
    }
}
