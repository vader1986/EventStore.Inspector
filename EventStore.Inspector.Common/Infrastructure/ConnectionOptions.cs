using System;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; }

        public bool ReadForward { get; }

        public int BatchSize { get; }

        public BatchMode BatchMode { get; }

        public TimeSpan? BatchSleepInterval { get; }

        public ConnectionOptions(string connectionString, bool readForward, int batchSize, BatchMode batchMode, TimeSpan? batchSleepInterval)
        {
            ConnectionString = connectionString;
            ReadForward = readForward;
            BatchMode = batchMode;
            BatchSleepInterval = batchSleepInterval;
        }
    }
}
