namespace CloudPrototyper.Model.Applications
{
    /// <summary>
    /// Triggers action when MessageType is added to queue named QueueName.
    /// </summary>
    public class MessageReceivedTrigger : Trigger
    {
        /// <summary>
        /// Message type triggering action.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Name of queue.
        /// </summary>
        public string QueueName { get; set; }
    }
}
