using System.Collections.Generic;
using System.Linq;

namespace CloudPrototyper.Model.Entities
{
    /// <summary>
    /// Class representing any data entity in the prototype model
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Unique name of the entity
        /// </summary>
        public string Name { get; set; }
        public string Description { get; set; }


        private List<PropertyInfo> _properties;
        /// <summary>
        /// Properties of the entity
        /// </summary>
        public List<PropertyInfo> Properties {

            get
            {
                if (!_properties.Select(x => x.Name).Contains("Id"))
                {
                    _properties.Add(new Model.Entities.PropertyInfo
                    {
                        ContentSize = 9,
                        IsIndexed = true,
                        IsReference = false,
                        IsMany = false,
                        Name = "Id",
                        Type = "System.Int32"
                    });
                }
                return _properties;
            }


            set => _properties = value;
        }
        

        public Entity()
        {
            Properties = new List<PropertyInfo>();
        }
    }
}
