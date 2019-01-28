using System;
using System.Collections.Generic;
using CloudPrototyper.Interface.Generation.Informations;
using CloudPrototyper.NET.Interface.Generation;

namespace CloudPrototyper.DataGenerator
{
    /// <summary>
    /// Adapts generated IStorage to runtime known IStorage of the tool.
    /// </summary>
    public class StorageInterfaceAdapter : IStorage
    {
        private readonly dynamic _storage;
        public StorageInterfaceAdapter(object toAdapt)
        {
            _storage = toAdapt;
        }
        public string GetName()
        {
            return _storage.GetName();
        }

        public List<object> GetEntities(string entitySetName, string entityName, Query query)
        {
            return _storage.GetEntities(entitySetName, entityName, query);
        }
        
        public void InsertAll(string entitySetName, string entityName, object[] toInsert)
        {
            try
            {
                _storage.InsertAll(entitySetName, entityName, toInsert);
            }
            catch (Exception ex)
            {
                 System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        public void Insert(string entitySetName, string entityName, object toInsert)
        {
            _storage.Insert(entitySetName, entityName, toInsert);
        }
    }
}
