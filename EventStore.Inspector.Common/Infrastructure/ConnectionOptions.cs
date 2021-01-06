using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.Inspector.Common.Infrastructure.Throttling;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionOptions
    {
        public ValueTask<IEventStoreConnection> Connection { get; }

        public bool ReadForward { get; }

        public int BatchSize { get; }

        public ThrottleOptions ThrottleOptions { get; }

        private ConnectionOptions(bool readForward, int batchSize, ThrottleOptions throttleOptions)
        {
            ReadForward = readForward;
            BatchSize = batchSize;
            ThrottleOptions = throttleOptions;
        }

        public ConnectionOptions(string connectionString, bool readForward, int batchSize, ThrottleOptions throttleOptions) : this(readForward, batchSize, throttleOptions)
        {
            Connection = ConnectionFactory.Create(connectionString);
        }

        public ConnectionOptions(IEventStoreConnection connection, bool readForward, int batchSize, ThrottleOptions throttleOptions) : this(readForward, batchSize, throttleOptions)
        {
            Connection = new ValueTask<IEventStoreConnection>(connection);
        }
    }
}
