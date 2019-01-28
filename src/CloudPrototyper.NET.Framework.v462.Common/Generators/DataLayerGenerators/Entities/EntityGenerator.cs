using System;
using System.Collections.Generic;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.Entities;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities
{
    /// <summary>
    /// Generator for entity classes.
    /// </summary>
    public class EntityGenerator : Modeled<Entity>
    {
        public EntityInterfaceGenerator EntityInterface { get; set; }
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
                    "EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL",
                    @"..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.dll"),
                new Tuple<string, string>(
                    "EntityFramework.SqlServer, Version = 6.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089, processorArchitecture = MSIL",
                    @"..\packages\EntityFramework.6.1.3\lib\net45\EntityFramework.SqlServer.dll")
            }, "EntityFramework", "6.1.3", "net462"),

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

        public EntityGenerator(string projectName, EntityInterfaceGenerator entityInterfaceGenerator, Entity modelParameters) : base(projectName, "Entities", modelParameters.Name, typeof(EntityTemplate), modelParameters, modelParameters.Name)
        {
            EntityInterface = entityInterfaceGenerator;
        }
    }
}
