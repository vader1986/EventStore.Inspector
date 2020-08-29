using System;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; }

        public bool ReadForward { get; }

        public int BatchSize { get; }

        public BatchMode BatchMode { get; }

        public int BatchSleepInterval { get; }

        public ConnectionOptions(string connectionString, bool readForward, int batchSize, BatchMode batchMode, int sleepIntervalMilliSeconds)
        {
            ConnectionString = connectionString;
            ReadForward = readForward;
            BatchMode = batchMode;
            BatchSize = batchSize;
            BatchSleepInterval = sleepIntervalMilliSeconds;
        }
    }
}
