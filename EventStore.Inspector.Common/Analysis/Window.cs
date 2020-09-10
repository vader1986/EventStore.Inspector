namespace EventStore.Inspector.Common.Analysis
{
    /// <summary>
    /// Mode for collecting events within a window for analysing.
    /// </summary>
    public enum Window
    {
        /// <summary>
        /// All events created within a time frame. 
        /// </summary>
        TimeFrame,

        /// <summary>
        /// Window specified in number of events.
        /// </summary>
        Events
    }
}
