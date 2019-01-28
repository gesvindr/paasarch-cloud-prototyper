using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudPrototyper.Interface;
using CloudPrototyper.Interface.Constants;
using CloudPrototyper.Interface.Generation;
using CloudPrototyper.Interface.Prototyper;
using CloudPrototyper.Model;
using CloudPrototyper.Model.Operations.DataAccess;
using CloudPrototyper.Model.Resources.Storage;
using CloudPrototyper.NET.Interface.Generation;
using CloudPrototyper.NET.Interface.Prototyper;

namespace CloudPrototyper.DataGenerator
{
    /// <summary>
    /// Using generated data layer of all application data are generated using operations requirements. The rest is generated randomly to fill
    /// entity storages to match their desired size.
    /// </summary>
    public class CustomDataGenerator : IEntityGenerator
    {
        private Prototype _prototype;
        private GeneratorManager _generatorManager;
        private IConfigProvider _configProvider;
        private readonly Dictionary<IStorage, Dictionary<EntitySet, EntityFactory>> _storages = new Dictionary<IStorage, Dictionary<EntitySet,EntityFactory>>();
        public List<EntityStorage> Setup(Prototype prototype, GeneratorManager generatorManager, IConfigProvider configProvider)
        {
            _prototype = prototype;
            _generatorManager = generatorManager;
            _configProvider = configProvider;
            CopyTypes();
            return InitTypes();
        }

        private void CopyTypes()
        {
            foreach (string dirPath in Directory.GetDirectories(_generatorManager.OutputBuildPath + "\\build", "*",
                SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(_generatorManager.OutputBuildPath + "\\build", AppDomain.CurrentDomain.BaseDirectory));
            }

            foreach (string newPath in Directory.GetFiles(_generatorManager.OutputBuildPath + "\\build", "*.*",
                SearchOption.AllDirectories))
            {
                var to = newPath.Replace(_generatorManager.OutputBuildPath + "\\build",
                    AppDomain.CurrentDomain.BaseDirectory);

                try
                {
                    if (!Path.GetFileName(to).StartsWith("DataLayer"))
                    {
                        File.Copy(newPath, to, true);
                        File.SetAttributes(to, FileAttributes.Normal);
                    }
                }
                catch (Exception)
                {
                    // File is used, cannot be replace, we have to replace old.
                }

            }
        }

        /// <summary>
        /// Triggers data generation
        /// </summary>
        public void Generate()
        {
            GenerateData();
        }

        private void GenerateData()
        {
            foreach (var storage in _storages.Keys)
            {
                foreach (var entitySet in _storages[storage].Keys)
                {
                    {
                        IEnumerable<List<object>> entities;
                        EntityFactory factory;
                        lock (_storages)
                        {
                            factory = _storages[storage][entitySet];
                            entities = factory.GetEntities();
                        }
                        foreach (var batch in entities)
                        {
                            lock (storage)
                            {
                                storage.InsertAll(entitySet.Name, factory.Entity.Name, batch.ToArray());
                            }
                        }
                    }
                 }
            }
        }
    

        private List<EntityStorage> InitTypes()
        {
            List<EntityStorage> output = new List<EntityStorage>();

            foreach (var storage in _generatorManager.GetRequiredResources().OfType<EntityStorage>())
            {
                var modeledStorage = _generatorManager.GetGenerable()
                    .Files.OfType<Modelable>()
                    .FirstOrDefault(x => x.Key.Equals(storage.Name));
                var adapter = new StorageInterfaceAdapter(GetInstanceFromGenerator(modeledStorage));
                _storages.Add(adapter, new Dictionary<EntitySet, EntityFactory>());
                foreach (var entitySet in storage.EntitySets)
                {
                    var entity = _prototype.Entities.Single(x => x.Name == entitySet.EntityName);
                    var modeledEntity = _generatorManager.GetGenerable()
                            .Files.OfType<Modelable>()
                            .FirstOrDefault(x => x.Key.Equals(entity.Name));
                    _storages[adapter].Add(entitySet, new EntityFactory(
                        entity, 
                        GetTypeFromGenerator(modeledEntity), 
                        entitySet,
                        storage, 
                            Utils.FindAllInstances<LoadEntitiesFromEntityStorage>(_prototype).Where(x => x.EntitySetName == entitySet.Name && x.EntityStorageName == storage.Name && x.EntityName == entity.Name).ToList()));
                        output.Add(storage);
                    
                }
            }

            return output;
        }

        private Type GetTypeFromGenerator(ICodeFile file)
        {
            var types = Utils.LoadTypes(_generatorManager.OutputBuildPath + "\\build");
            return types.Single(x => x.Name == file.Name);
        }
        private object GetInstanceFromGenerator(ICodeFile file)
        {
            var types = Utils.LoadTypes(_generatorManager.OutputBuildPath + "\\build");
            Type type = types.Single(x => x.Name == file.Name);
            return Activator.CreateInstance(type);
        }
    }
}
