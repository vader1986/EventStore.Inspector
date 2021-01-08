using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Embedded;
using EventStore.Common.Options;
using EventStore.Core;
using EventStore.Inspector.Testing.Bus;
using EventStore.Inspector.Testing.Events;

namespace EventStore.Inspector.Testing
{
    public class EventStoreRunner : IDisposable
    {
        private readonly ClusterVNode _cluster;
        private readonly EventStoreAppender _appender;
        private readonly EventQueue _queue;
        private bool _isDisposed;

        public IEventStoreConnection Connection { get; }

        private EventStoreRunner(IEventStoreConnection connection, ClusterVNode cluster)
        {
            Connection = connection;
            _cluster = cluster;
            _appender = new EventStoreAppender(connection);
            _queue = new EventQueue(new Bus<ResolvedEvent>(), connection);
        }

        public static async Task<EventStoreRunner> Start()
        {
            var node = EmbeddedVNodeBuilder
                .AsSingleNode()
                .OnDefaultEndpoints()
                .RunProjections(ProjectionType.All)
                .RunInMemory()
                .Build();

            var cluster = await node.StartAsync(true);

            return new EventStoreRunner(EmbeddedEventStoreConnection.Create(node), cluster);
        }

        public async Task Write(EventWrapper @event)
        {
            await _appender.Append(@event);

            _queue.WaitForNext(re => re.Event != null &&
                                     re.Event.EventType == @event.EventType &&
                                     re.Event.EventId == @event.Id);
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Connection.Dispose();
                _cluster.StopAsync().Wait(TimeSpan.FromSeconds(5));
                _queue.Dispose();
                _isDisposed = true;
            }
        }
    }
}
