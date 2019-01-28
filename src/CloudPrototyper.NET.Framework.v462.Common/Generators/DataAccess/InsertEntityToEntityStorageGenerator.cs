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
    /// Generator of operation inserting entities to entity storage.
    /// </summary>
    public class InsertEntityToEntityStorageGenerator : Modeled<InsertEntityToEntityStorage>, IOperation
    {
        public override List<PackageConfigInfo> GetNugetPackages()
        {
            return NugetFactory.MakeEntityFrameworkNuget();
        }
        public OperationInterfaceGenerator OperationInterface { get; set; }
        public StorageInterfaceGenerator StorageInterface { get; set; }
        public Modelable Storage { get; }
        public QueryGenerator Query { get; set; }
        public InsertEntityToEntityStorageGenerator(string projectName, OperationInterfaceGenerator operationInterface, StorageInterfaceGenerator storageInterface,
            InsertEntityToEntityStorage modelParameters, IList<Modelable> storages, QueryGenerator query)
            : base(
                projectName, "Operations", modelParameters.Name, typeof (InsertEntityToEntityStorageTemplate),
                modelParameters, modelParameters.Name)
        {
            Query = query;
            Storage = storages.Single(x => x.Key.Equals(modelParameters.EntityStorageName));
            OperationInterface = operationInterface;
            StorageInterface = storageInterface;
        } 
    }
}
