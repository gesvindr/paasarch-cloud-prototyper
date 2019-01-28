using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.Model.Operations.DataAccess;
using CloudPrototyper.Model.Resources.Storage;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace CloudPrototyper.DataGenerator
{
    /// <summary>
    /// Generates entities based on requirements.
    /// </summary>
    public class EntityFactory
    {
        public const int BatchSize = 100;
        private readonly Type _entityType;
        private EntityStorage _entityStorage;
        private readonly List<LoadEntitiesFromEntityStorage> _operations;
        private EntitySet _entitySet;
        private int _spaceLeft;
        private readonly Dictionary<LoadEntitiesFromEntityStorage, List<object>> _generateDictionary = new Dictionary<LoadEntitiesFromEntityStorage, List<object>>();
        private readonly List<object> _generatedDummyData = new List<object>();
        public Entity Entity { get; set; }
        public EntityFactory(Entity entity, Type entityType, EntitySet entitySet, EntityStorage storage, List<LoadEntitiesFromEntityStorage> operations)
        {
            _entitySet = entitySet;
            _entityType = entityType;
            _entityStorage = storage;
            _operations = operations;
            Entity = entity;
            _spaceLeft = entitySet.Count - operations.Sum(x => x.Filter.NumberOfResults);
            if (_spaceLeft < 0)
            {
                throw new ArgumentException("Entity set is too small to satisfy operations requirements");
            }
            foreach (var operation in operations)
            {
                _generateDictionary.Add(operation, new List<object>());
            }
        }

        /// <summary>
        /// Yield returns entities based on requirements.
        /// </summary>
        /// <returns>Generated entities.</returns>
        public IEnumerable<List<object>> GetEntities()
        {

            while (_spaceLeft != 0)
            {
                var batch = GetBatch(Math.Min(_spaceLeft, BatchSize), _entityType, Entity, _generateDictionary);
                _spaceLeft -= batch.Count;
                _generatedDummyData.AddRange(batch);
                yield return batch;
            }
            foreach (var operation in _operations)
            {
                var uniqueBatch = GetUniqueBachSetOperation(_generateDictionary, operation, Entity, _entityType, _generatedDummyData);
                _spaceLeft -= uniqueBatch.Count;
                _generateDictionary[operation].AddRange(uniqueBatch);
                yield return uniqueBatch;
            }
        }

        private static List<object> GetUniqueBachSetOperation(Dictionary<LoadEntitiesFromEntityStorage, List<object>> generated, LoadEntitiesFromEntityStorage operation, Entity entity, Type entityType, List<object> dummyData)
        {
            if (operation.Filter.UseKey || String.IsNullOrEmpty(operation.Filter.OnAttribute))
            {
                operation.Filter.OnAttribute = "Id";
                operation.Filter.IsNominal = false;
            }
            if (operation.Filter.IsNominal)
            {
                return CreateNominal(generated, operation, entity, entityType, dummyData);
            }
            return CreateOrdinal(generated, operation, entity, entityType, dummyData);
        }


        private static List<object> GetBatch(int count, Type entityType, Entity entity, Dictionary<LoadEntitiesFromEntityStorage, List<object>> cannotInterfereWith)
        {
            var output = new List<object>();

            while (output.Count < count)
            {
                var randomed = FillEntity(CreateInstance(entityType), entity);
                {
                    output.Add(randomed);
                }
            }

            return output;
        }
        private static bool IsInterfered(object randomed, Dictionary<LoadEntitiesFromEntityStorage, List<object>> cannotInterfereWith, List<object> andWith)
        {
            foreach (var key in cannotInterfereWith.Keys)
            {
                foreach (var obj in cannotInterfereWith.Values.SelectMany(x=>x))
                {
                    if (Overlap(obj, randomed, key.Filter.OnAttribute, key.Filter.IsNominal, key.Filter) || Overlap(obj, randomed, "Id", true, null))
                    {
                        return true;
                    }
                }
                foreach (var obj in andWith)
                {
                    if (Overlap(obj, randomed, key.Filter.OnAttribute, key.Filter.IsNominal, key.Filter) || Overlap(obj, randomed, "Id", true, null))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private static bool Overlap(object obj, object randomed, string filterOnAttribute, bool nominal, FilterCondition filer)
        {
            if (nominal)
            {
                return GetValue(obj, filterOnAttribute).Equals(GetValue(randomed, filterOnAttribute));
            }
            var value = (dynamic)GetValue(randomed, filterOnAttribute);

            return !(value.ToString().CompareTo(filer.MaxValue) > 0 ||
                     value.ToString().CompareTo(filer.MinValue) < 0);
        }

        private static object GetValue(object obj, string filterOnAttribute)
        {
            var propertyInfo = obj.GetType().GetProperty(filterOnAttribute);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(obj, null);
            }

            return null;
        }

        private static List<object> CreateOrdinal(Dictionary<LoadEntitiesFromEntityStorage, List<object>> generated, LoadEntitiesFromEntityStorage operation, Entity entity, Type entityType, List<object> dummyData)
        {
            var value = new ContentFactory().GetData(Type.GetType(entity.Properties.Single(x => x.Name == operation.Filter.OnAttribute).Type)?.Name, entity.Properties.Single(x => x.Name == operation.Filter.OnAttribute).ContentSize);

            List<object> output = new List<object>();
            int n;
            bool isNumeric = int.TryParse(value.ToString(), out n);
            if (isNumeric)
            {
                operation.Filter.MinValue = value.ToString();
                operation.Filter.MaxValue = ((dynamic)value + operation.Filter.NumberOfResults).ToString();
                for (int i = 0; i < operation.Filter.NumberOfResults; i++)
                {
                    var filledInstance = FillEntity(CreateInstance(entityType), entity);
                    SetPropertyToValue(filledInstance, operation.Filter.OnAttribute, value);
                    value = ((dynamic)value) + 1;
                    if (!IsInterfered(filledInstance, generated.Where(x=>x.Key!=operation).ToDictionary(y=>y.Key, y=>y.Value), dummyData))
                    {
                        output.Add(filledInstance);
                    }
                    else
                    {
                        i--;
                    }
                }

                operation.Filter.MaxValue = value.ToString();
            }
            else
            {

                for (int i = 0; i < operation.Filter.NumberOfResults; i++)
                {
                    var filledInstance = FillEntity(CreateInstance(entityType), entity);
                    SetPropertyToValue(filledInstance, operation.Filter.OnAttribute, value);
                    if (!IsInterfered(filledInstance, generated.Where(x => x.Key != operation).ToDictionary(y => y.Key, y => y.Value), dummyData))
                    {
                        output.Add(filledInstance);
                    }
                    else
                    {
                        i--;
                    }
                }
                operation.Filter.MaxValue = value.ToString();
                operation.Filter.MinValue = value.ToString();
            }

          

            return output;
        }

        private static List<object> CreateNominal(Dictionary<LoadEntitiesFromEntityStorage, List<object>> generated, LoadEntitiesFromEntityStorage operation, Entity entity, Type entityType, List<object> dummyData)
        {
            var value = new ContentFactory().GetData(entity.Properties.Single(x => x.Name == operation.Filter.OnAttribute).Type,
                entity.Properties.Single(x => x.Name == operation.Filter.OnAttribute).ContentSize);

            List<object> output = new List<object>();


            for (int i = 0; i < operation.Filter.NumberOfResults; i++)
            {
                var filledInstance = FillEntity(CreateInstance(entityType), entity);
                SetPropertyToValue(filledInstance, operation.Filter.OnAttribute, value);
                if (!IsInterfered(filledInstance, generated, dummyData))
                {
                    output.Add(filledInstance);
                }
                else
                {
                    i--;
                }
            }

            operation.Filter.NominalValue = value.ToString();

            return output;
        }
        private static object FillEntity(object instace, Entity entityInforation)
        {
            foreach (var property in entityInforation.Properties)
            {
                SetPropertyToValue(instace, property.Name, GetContent(property.Type, property.ContentSize));
            }
            return instace;
        }

        private static object GetContent(string propertyType, int propertyContentSize)
        {
            var memberInfo = Type.GetType(propertyType);
            if (memberInfo != null)
            {
                return new ContentFactory().GetData(memberInfo.Name, propertyContentSize);
            }
            throw new ArgumentException("Type not supported");
        }

        private static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        private static void SetPropertyToValue(object obj, string propertyName, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
            }
            else
            {
                throw new ArgumentException("Invalid property name");
            }
        }
    }
}
