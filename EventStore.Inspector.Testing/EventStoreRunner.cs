using System;
using System.Collections.Generic;
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

        private EventStoreRunner(IEventStoreConnection connection, ClusterVNode cluster, IEnumerable<string> streams)
        {
            Connection = connection;
            _cluster = cluster;
            _appender = new EventStoreAppender(connection);
            _queue = new EventQueue(new Bus<ResolvedEvent>(), connection, streams);
        }

        /// <summary>
        /// Creates and runs a new instance of EventStoreDB locally in memory and
        /// subscribes to the specified streams.
        /// </summary>
        /// <param name="streams">List of all event streams <see cref="EventStoreRunner"/>
        /// ever <see cref="Write(EventWrapper)">writes</see> to.</param>
        /// <returns></returns>
        public static async Task<EventStoreRunner> Start(params string[] streams)
        {
            var node = EmbeddedVNodeBuilder
                .AsSingleNode()
                .OnDefaultEndpoints()
                .StartStandardProjections()
                .RunProjections(ProjectionType.All)
                .RunInMemory()
                .Build();

            var cluster = await node.StartAsync(true);
            var connection = EmbeddedEventStoreConnection.Create(node);

            await connection.ConnectAsync();

            return new EventStoreRunner(connection, cluster, streams);
        }

        /// <summary>
        /// Writes the specified event to the running ES instance. After appending
        /// the event to its target stream, <see cref="EventStoreRunner"/> waits
        /// until the event reached the subscription of the stream.
        /// </summary>
        /// <param name="event">Wrapper for the event to write to ES.</param>
        /// <returns>Task which completes once the new event has reached the corresponding
        /// ES subscription.</returns>
        /// <exception cref="ItemNotFoundException{ResolvedEvent}">Throws if there
        /// is no subscription for the specified event. Make sure you passed the
        /// stream name to the <see cref="Start(string[])"/> method!</exception>
        public async Task Write(EventWrapper @event)
        {
            await _appender.Append(@event);

            _queue.WaitForNext(re => re.Event.EventId == @event.Id);
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
