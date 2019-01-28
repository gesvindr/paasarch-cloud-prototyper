using System;
using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Azure.Resources.Storage;
using CloudPrototyper.Model.Applications;
using CloudPrototyper.NET.Framework.v462.Common.Templates.BusinessLayerTemplates.Services;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators.Services
{
    /// <summary>
    /// Service Queue Handeler.
    /// </summary>
    public class HandlerGenerator : Modeled<IList<TriggeredAction>>
    {
        public AzureServiceBusQueue AzureServiceBusQueue { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public IList<ActionGenerator> Actions { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {
            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Microsoft.ServiceBus, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL",
                    @"..\packages\WindowsAzure.ServiceBus.3.4.2\lib\net45-full\Microsoft.ServiceBus.dll")
            },"WindowsAzure.ServiceBus","3.4.2","net462")

        };
        public HandlerGenerator(string projectName, AzureServiceBusQueue azureServiceBusQueue,
            MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, IList<TriggeredAction> modelParameters, IList<ActionGenerator> actions , OperationInterfaceGenerator operationInterface)
            : base(
                projectName, "Services", azureServiceBusQueue.Name + "Handler", typeof (HandlerTemplate),
                modelParameters, azureServiceBusQueue.Name + "Handler")
        {
            Actions = actions.Where(x => modelParameters.Select(y => y.Name).Contains(x.Key)).ToList();
            OperationInterface = operationInterface;
            AzureServiceBusQueue = azureServiceBusQueue;
            MessageBusHandlerInterface = messageBusHandlerInterface;
        }
    }
}
