using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Embedded;
using EventStore.Core;
using EventStore.Inspector.Testing.Events;

namespace EventStore.Inspector.Testing
{
    public class EventStoreRunner : IDisposable
    {
        private readonly ClusterVNode _cluster;
        private readonly EventStoreAppender _appender;
        private bool _isDisposed;

        public IEventStoreConnection Connection { get; }

        private EventStoreRunner(IEventStoreConnection connection, ClusterVNode cluster)
        {
            Connection = connection;
            _cluster = cluster;
            _appender = new EventStoreAppender(connection);
        }

        public static async Task<EventStoreRunner> Start()
        {
            var node = EmbeddedVNodeBuilder
                .AsSingleNode()
                .OnDefaultEndpoints()
                .RunProjections(Common.Options.ProjectionType.All)
                .RunInMemory()
                .Build();

            var cluster = await node.StartAsync(true);

            return new EventStoreRunner(EmbeddedEventStoreConnection.Create(node), cluster);
        }

        public async Task Write(EventWrapper @event)
        {
            await _appender.Append(@event);
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Connection.Dispose();
                _cluster.StopAsync().Wait(TimeSpan.FromSeconds(5));
                _isDisposed = true;
            }
        }
    }
}
