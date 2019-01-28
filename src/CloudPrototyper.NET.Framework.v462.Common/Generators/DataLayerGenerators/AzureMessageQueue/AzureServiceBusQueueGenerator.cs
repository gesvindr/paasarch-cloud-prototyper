using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.DataFactories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.Entities;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataLayerTemplates.AzureMessageQueue;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataLayerGenerators.AzureMessageQueue
{ 
    /// <summary>
    /// Generator of azure service bus. 
    /// </summary>
    public class AzureServiceBusQueueGenerator : Modeled<AzureServiceBusQueue>
    {
        public MessageBusInterfaceGenerator MessageBusInterfaceGenerator { get; set; }
        public DataGeneratorGenerator DataGenerator { get; set; }
        public List<EntityGenerator> Entities { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Microsoft.ServiceBus, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                        @"..\packages\WindowsAzure.ServiceBus.3.4.2\lib\net45-full\Microsoft.ServiceBus.dll")
                },"WindowsAzure.ServiceBus","3.4.2","net462")
            
        };

        public AzureServiceBusQueueGenerator(string projectName, MessageBusInterfaceGenerator messageBusInterfaceGenerator, AzureServiceBusQueue modelParameters, DataGeneratorGenerator dataGenerator, IList<EntityGenerator> entities) : base(projectName, modelParameters.Name, modelParameters.Name+"Context", typeof(AzureServiceBusQueueTemplate), modelParameters, modelParameters.Name)
        {
            DataGenerator = dataGenerator;
            Entities = entities.ToList();
            MessageBusInterfaceGenerator = messageBusInterfaceGenerator;
        }
    }
}
