namespace EventStore.Inspector.Common.Infrastructure
{
    /// <summary>
    /// How to proceed after a batch of events has been processed.
    /// </summary>
    public enum BatchMode
    {
        /// <summary>
        /// Await user input before starting the next batch.
        /// </summary>
        AwaitUserInput,

        /// <summary>
        /// Sleep for a fixed amount of time after each batch.
        /// </summary>
        Sleep,

        /// <summary>
        /// Continue with the next batch right away. 
        /// </summary>
        Continue
    }
}
