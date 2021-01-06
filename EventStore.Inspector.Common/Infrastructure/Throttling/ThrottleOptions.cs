namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    public class ThrottleOptions
    {
        public BatchMode BatchMode { get; }

        public int SleepIntervalMilliSeconds { get; }

        public ThrottleOptions(BatchMode batchMode, int sleepIntervalMilliSeconds)
        {
            BatchMode = batchMode;
            SleepIntervalMilliSeconds = sleepIntervalMilliSeconds;
        }
    }
}
