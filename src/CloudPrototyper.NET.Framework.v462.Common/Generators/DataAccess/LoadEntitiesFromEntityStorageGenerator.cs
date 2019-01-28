using System.Collections.Generic;
using System.Linq;
using CloudPrototyper.Model.Operations.DataAccess;
using CloudPrototyper.NET.Framework.v462.Common.Factories;
using CloudPrototyper.NET.Framework.v462.Common.Templates.DataAccess;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.NET.Framework.v462.Common.Generators.DataAccess
{
    /// <summary>
    /// Generator of opeation loading entities from entity storage.
    /// </summary>
    public class LoadEntitiesFromEntityStorageGenerator : Modeled<LoadEntitiesFromEntityStorage>, IOperation
    {
        public override List<PackageConfigInfo> GetNugetPackages()
        {
            return NugetFactory.MakeEntityFrameworkNuget();
        }

        public OperationInterfaceGenerator OperationInterface { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public Modelable Storage { get; }
        public QueryGenerator Query { get; set; }
        public LoadEntitiesFromEntityStorageGenerator(string projectName, QueryGenerator query, StorageInterfaceGenerator storageInterface, OperationInterfaceGenerator operationInterface, LoadEntitiesFromEntityStorage modelParameters, IList<Modelable> storages) : base(projectName, "Operations", modelParameters.Name, typeof(LoadEntitiesFromEntityStorageTemplate), modelParameters, modelParameters.Name)
        {
            Query = query;
            OperationInterface = operationInterface;
            Storage = storages.Single(x => x.Key.Equals(modelParameters.EntityStorageName));
            StorageInterface = storageInterface;
        }
    }
}
