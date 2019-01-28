namespace CloudPrototyper.Model.Applications
{
    /// <summary>
    /// Action triggered by Trigger.
    /// </summary>
    public class TriggeredAction : Action
    {
        /// <summary>
        /// Custion trigger of action.
        /// </summary>
        public Trigger Trigger { get; set; }
    }
}
