using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.TblStorage.Model;
using CloudPrototyper.NET.Framework.v462.TblStorage.Templates;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;
using QueryGenerator = CloudPrototyper.NET.Interface.Generation.QueryGenerator;

namespace CloudPrototyper.NET.Framework.v462.TblStorage.Generators
{
    /// <summary>
    /// Generator of Azure table storage context.
    /// </summary>
    public class AzureTableStorageContextGenerator : Modeled<AzureTableStorage>, IEntityStorage
    {
        public List<EntityGenerator> Entities { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public QueryGenerator Query { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Azure.KeyVault.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Azure.KeyVault.Core.1.0.0\lib\net40\Microsoft.Azure.KeyVault.Core.dll")
            }, "Microsoft.Azure.KeyVault.Core", "1.0.0", "net462"),
            
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Data.Edm, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Data.Edm.5.6.4\lib\net40\Microsoft.Data.Edm.dll")
            }, "Microsoft.Data.Edm", "5.6.4", "net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Data.OData, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\Microsoft.Data.OData.5.6.4\lib\net40\Microsoft.Data.OData.dll")
            }, "Microsoft.Data.OData", "5.6.4", "net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.Data.Services.Client, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"
                    ,
                    @"..\packages\Microsoft.Data.Services.Client.5.6.4\lib\net40\Microsoft.Data.Services.Client.dll")
            }, "Microsoft.Data.Services.Client", "5.6.4", "net462"),



            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Newtonsoft.Json, Version=6.0.0.0, Culture=neutral, " +
                                          "PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\packages\Newtonsoft.Json.6.0.8\lib\net45\Newtonsoft.Json.dll")
            }, "Newtonsoft.Json", "6.0.8", "net462"),


            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "System.Spatial, Version=5.6.4.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\System.Spatial.5.6.4\lib\net40\System.Spatial.dll")
            }, "System.Spatial", "5.6.4", "net462"),



            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.WindowsAzure.Storage, Version=7.2.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\WindowsAzure.Storage.7.2.1\lib\net40\Microsoft.WindowsAzure.Storage.dll")
            }, "WindowsAzure.Storage", "7.2.1", "net462"),

        };

        public AzureTableStorageContextGenerator(string projectName, AzureTableStorage modelParameters, IList<EntityGenerator> entities,
            StorageInterfaceGenerator storageInterface, QueryGenerator query, DataGeneratorGenerator dataGenerator)
            : base(
                projectName, modelParameters.Name, modelParameters.Name+"Context", typeof(AzureTableStorageContextTemplate),
                modelParameters, modelParameters.Name)
        {
            Query = query;
            Entities = entities.Where(x => modelParameters.EntitySets.Select(y => y.EntityName).ToList().Contains(x.Name)).ToList();
            StorageInterface = storageInterface;
            DataGenerator = dataGenerator;
        }
    }
}
