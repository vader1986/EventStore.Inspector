using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionWrapper : IConnectionWrapper
    {
        private Lazy<Task<IEventStoreConnection>> _connection;

        public ConnectionWrapper(ConnectionOptions options)
        {
            _connection = ConnectionFactory.Create(options.ConnectionString);
        }

        public async Task ConnectToStreamAsync(string stream, Action<IEventRecord> onEventRead)
        {
            var connection = await _connection.Value;
            var events = await connection.ReadStreamEventsForwardAsync(stream, StreamPosition.Start, 1000, true);

            if (events != null && events.Events != null)
            {
                foreach (var resolvedEvent in events.Events)
                {
                    onEventRead?.Invoke(EventRecord.From(resolvedEvent));
                }
            }
        }
    }
}
