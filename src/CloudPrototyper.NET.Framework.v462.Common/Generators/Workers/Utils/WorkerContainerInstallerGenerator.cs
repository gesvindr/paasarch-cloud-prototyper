using System;
using System.Collections.Generic;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Templates.Workers.Utils;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.Workers.Utils
{
    /// <summary>
    /// Worker project IoC container instalator.
    /// </summary>
    public class WorkerContainerInstallerGenerator : CodeGeneratorBase
    {
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public MessageBusInterfaceGenerator MessageBusInterface { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public ActionBaseGenerator ActionBase { get; set; }
        public override List<PackageConfigInfo> GetNugetPackages() => new List<PackageConfigInfo>
        {

            new PackageConfigInfo(new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    "Castle.Windsor, Version=3.4.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL",
                    @"..\packages\Castle.Windsor.3.4.0\lib\net45\Castle.Windsor.dll")
            }, "Castle.Windsor", "3.4.0", "net462"),


            new PackageConfigInfo(

                new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Castle.Core, Version=3.3.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL",
                        @"..\packages\Castle.Core.3.3.0\lib\net45\Castle.Core.dll")
                }, "Castle.Core", "3.3.0", "net462")

        };

        public WorkerContainerInstallerGenerator(string projectName, StorageInterfaceGenerator storageInterface, MessageBusInterfaceGenerator messageBusInterface, MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, OperationInterfaceGenerator operationInterface, ActionBaseGenerator actionBase, bool canInitialize = true) : base(projectName, "Utils", "Installer", typeof(WorkerContainerInstallerTemplate), canInitialize)
        {
            ActionBase = actionBase;
            StorageInterface = storageInterface;
            MessageBusInterface = messageBusInterface;
            MessageBusHandlerInterface = messageBusHandlerInterface;
            OperationInterface = operationInterface;
        }
    }
}
