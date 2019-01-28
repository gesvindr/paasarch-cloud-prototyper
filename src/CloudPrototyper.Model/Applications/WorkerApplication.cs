using System.Collections.Generic;

namespace CloudPrototyper.Model.Applications
{
    /// <summary>
    /// Background worker application.
    /// </summary>
    public class WorkerApplication : Application
    {
        /// <summary>
        /// List of triggered actions within application.
        /// </summary>
        public List<TriggeredAction> Actions { get; set; }
    }
}
