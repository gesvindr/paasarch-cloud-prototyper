using CloudPrototyper.Model.Operations;

namespace CloudPrototyper.Model.Applications
{
    /// <summary>
    /// Action triggering operation.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Unique name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Operation to be triggered.
        /// </summary>
        public Operation Operation { get; set; }
        
    }
}
