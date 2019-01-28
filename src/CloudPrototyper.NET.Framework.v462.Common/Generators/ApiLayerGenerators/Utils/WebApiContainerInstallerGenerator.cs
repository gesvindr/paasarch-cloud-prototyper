using System;
using System.Collections.Generic;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Templates.ApiLayerTemplates.Utils;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.ApiLayerGenerators.Utils
{
    /// <summary>
    /// Web Api Container Installer.
    /// </summary>
    public class WebApiContainerInstallerGenerator : CodeGeneratorBase
    {
        public ActionBaseGenerator ActionBase { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public MessageBusInterfaceGenerator MessageBusInterface { get; set; }
        public MessageBusHandlerInterfaceGenerator MessageBusHandlerInterface { get; set; }
        public OperationInterfaceGenerator OperationInterface { get; set; }

        public override List<PackageConfigInfo> GetNugetPackages()
        {
            var output = new List<PackageConfigInfo>();
            output.AddRange(
                NugetFactory.MakeEntityFrameworkNuget());

            output.Add
            (
                new PackageConfigInfo(new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(
                        "Castle.Windsor, Version=3.4.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL",
                        @"..\packages\Castle.Windsor.3.4.0\lib\net45\Castle.Windsor.dll")
                }, "Castle.Windsor", "3.4.0", "net462")

            );
            output.Add
            (
                new PackageConfigInfo(

                    new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>(
                            "Castle.Core, Version=3.3.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc, processorArchitecture=MSIL",
                            @"..\packages\Castle.Core.3.3.0\lib\net45\Castle.Core.dll")
                    }, "Castle.Core", "3.3.0", "net462")
            );
            return output;
        }

        public WebApiContainerInstallerGenerator(string projectName, StorageInterfaceGenerator storageInterface, MessageBusInterfaceGenerator messageBusInterface, MessageBusHandlerInterfaceGenerator messageBusHandlerInterface, OperationInterfaceGenerator operationInterface, ActionBaseGenerator actionBase, bool canInitialize = true) : base(projectName, "Utils", "Installer", typeof(WebApiContainerInstallerTemplate), canInitialize)
        {
            StorageInterface = storageInterface;
            MessageBusInterface = messageBusInterface;
            MessageBusHandlerInterface = messageBusHandlerInterface;
            OperationInterface = operationInterface;
            ActionBase = actionBase;
        }
    }
}
