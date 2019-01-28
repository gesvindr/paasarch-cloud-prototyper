﻿using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CloudPrototyper.NET.Framework.v462.Common.Generators.BusinessLayerGenerators;
using CloudPrototyper.NET.Framework.v462.Common.Generators.Workers.Utils;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.NET.Framework.v462.Common.Registrations.WorkersRegistrations.Utils
{
    /// <summary>
    /// Worker installer registrations.
    /// </summary>
    public class WorkerContainerInstallerRegistrations : GeneratorDependency<WorkerContainerInstallerGenerator>
    {
        public override List<IRegistration> GetRegistrations(string projectName)
       => new List<IRegistration>
       {
                Component.For<StorageInterfaceGenerator>()
                    .ImplementedBy<StorageInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                 Component.For<MessageBusInterfaceGenerator>()
                    .ImplementedBy<MessageBusInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                  Component.For<MessageBusHandlerInterfaceGenerator>()
                    .ImplementedBy<MessageBusHandlerInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                  Component.For<OperationInterfaceGenerator>()
                    .ImplementedBy<OperationInterfaceGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName)),
                    Component.For<ActionBaseGenerator>()
                    .ImplementedBy<ActionBaseGenerator>()
                    .LifestyleSingleton()
                    .DependsOn(Dependency.OnValue("projectName", projectName))
       };
    }
}
