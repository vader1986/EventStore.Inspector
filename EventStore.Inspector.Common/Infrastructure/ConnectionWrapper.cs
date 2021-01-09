using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.Inspector.Common.Infrastructure.Throttling;
using EventStore.Inspector.Common.Support;
using Serilog;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionWrapper : IConnectionWrapper, IDisposable
    {
        private const bool ResolveLinkTos = true;

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<ConnectionWrapper>();

        private readonly AsyncLazy<IEventStoreConnection> _connection;
        private readonly ConnectionOptions _settings;
        private readonly IThrottle _throttle;

        public ConnectionWrapper(ConnectionOptions options, IThrottleFactory? throttleFactory = default)
        {
            throttleFactory = throttleFactory ?? new ThrottleFactory();

            _settings = options;
            _connection = new AsyncLazy<IEventStoreConnection>(() => options.Connection);
            _throttle = throttleFactory.Create(options.ThrottleOptions);
        }

        private async Task<StreamEventsSlice> ReadStream(IEventStoreConnection connection, string stream, long offset)
        {
            if (_settings.ReadForward)
            {
                return await connection.ReadStreamEventsForwardAsync(stream, offset, _settings.BatchSize, ResolveLinkTos);
            }
            else
            {
                return await connection.ReadStreamEventsBackwardAsync(stream, offset, _settings.BatchSize, ResolveLinkTos);
            }
        }

        private long InitialStreamPosition()
        {
            return _settings.ReadForward ? StreamPosition.Start : StreamPosition.End;
        }

        public async Task ConnectToStreamAsync(string stream, Action<IEventRecord> onEventRead)
        {
            var connection = await _connection;
            var offset = InitialStreamPosition();

            StreamEventsSlice events;

            do
            {
                events = await ReadStream(connection, stream, offset);

                if (events?.Events != null)
                {
                    foreach (var resolvedEvent in events.Events)
                    {
                        onEventRead?.Invoke(EventRecord.From(resolvedEvent));
                    }

                    if (events.NextEventNumber == -1)
                    {
                        break;
                    }

                    offset = events.NextEventNumber;
                        
                    if (offset < 0)
                    {
                        Log.Warning("Invalid event number {NextEventNumber} for next batch", offset);
                    }
                }
                else
                {
                    Log.Error("Failed to read events at {Stream}/{EventNumber}", stream, offset);
                    break;
                }

                await _throttle.Throttle();
            }
            while (!events.IsEndOfStream);
        }

        public void Dispose()
        {
            _connection.Value.GetAwaiter().GetResult().Dispose();
        }
    }
}
