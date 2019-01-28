using CloudPrototyper.Model.Applications;
using CloudPrototyper.Model.Entities;
using CloudPrototyper.Model.Resources;
using System.Collections.Generic;

namespace CloudPrototyper.Model
{
    /// <summary>
    /// Root object of model with applications, entities and resources.
    /// </summary>
    public class Prototype
    {
        /// <summary>
        /// Applications.
        /// </summary>
        public List<Application> Applications { get; set; }

        /// <summary>
        /// Entities.
        /// </summary>
        public List<Entity> Entities { get; set; }

        /// <summary>
        /// Resources.
        /// </summary>
        public List<Resource> Resources { get; set; }
        public Prototype()
        {

        }
    }
}
