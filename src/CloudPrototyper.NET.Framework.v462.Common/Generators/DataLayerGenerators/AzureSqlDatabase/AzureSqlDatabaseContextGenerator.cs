using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.AzureSql;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;
using QueryGenerator = CloudPrototyper.NET.Interface.Generation.QueryGenerator;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.AzureSqlDatabase
{
    /// <summary>
    /// Azure sql database representation generator.
    /// </summary>
    public class AzureSqlDatabaseContextGenerator : Modeled<AzureSQLDatabase>, IEntityStorage
    {
        public List<EntityGenerator> Entities { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }
        public EntityInterfaceGenerator EntityInterface { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public QueryGenerator Query{ get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL",
                    @"..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.dll"),
                new Tuple<string, string>(
                    "EntityFramework.SqlServer, Version = 6.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089, processorArchitecture = MSIL",
                    @"..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.SqlServer.dll")
            }, "EntityFramework", "6.1.3", "net462")

        };

        public AzureSqlDatabaseContextGenerator(string projectName, QueryGenerator query, IList<EntityGenerator> entities, EntityInterfaceGenerator entityInterface, StorageInterfaceGenerator storageInterface, AzureSQLDatabase modelParameters, DataGeneratorGenerator dataGenerator) : base(projectName, modelParameters.Name, modelParameters.Name+ "AzureSqlDatabaseContext", typeof(AzureSqlDatabaseContextTemplate), modelParameters, modelParameters.Name)
        {
            DataGenerator = dataGenerator;
            StorageInterface = storageInterface;
            Query = query;
            Entities = entities.Where(x => modelParameters.EntitySets.Select(y => y.EntityName).ToList().Contains(x.Name)).ToList();
            EntityInterface = entityInterface;
        }
    }
}
