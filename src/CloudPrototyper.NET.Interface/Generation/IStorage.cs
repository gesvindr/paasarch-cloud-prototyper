using System.Collections.Generic;
using CloudPrototyper.Interface.Generation.Informations;

namespace CloudPrototyper.NET.Interface.Generation
{
    public interface IStorage
    {
        string GetName();
        List<object> GetEntities(string entitySetName, string entityName, Query query);
        void InsertAll(string entitySetName, string entityName, object[] toInsert);
        void Insert(string entitySetName, string entityName, object toInsert);
    }
}
