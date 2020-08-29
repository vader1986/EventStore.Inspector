using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Serilog;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionWrapper : IConnectionWrapper
    {
        private const bool ResolveLinkTos = true;

        private readonly Serilog.ILogger _log = Log.ForContext<ConnectionWrapper>();
        private readonly Lazy<Task<IEventStoreConnection>> _connection;
        private readonly ConnectionOptions _settings;

        public ConnectionWrapper(ConnectionOptions options)
        {
            _settings = options;
            _connection = ConnectionFactory.Create(options.ConnectionString);
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

        private async Task BatchBreak()
        {
            switch (_settings.BatchMode)
            {
                case BatchMode.AwaitUserInput:
                    await Console.In.ReadLineAsync();
                    break;

                case BatchMode.Continue:
                    break;

                case BatchMode.Sleep:
                    await Task.Delay(_settings.BatchSleepInterval);
                    break;
            }
        }

        public async Task ConnectToStreamAsync(string stream, Action<IEventRecord> onEventRead)
        {
            var connection = await _connection.Value;
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
                        _log.Warning("Invalid event number {NextEventNumber} for next batch", offset);
                    }
                }
                else
                {
                    _log.Error("Failed to read events at {Stream}/{EventNumber}", stream, offset);
                    break;
                }

                await BatchBreak();
            }
            while (!events.IsEndOfStream);
        }
    }
}
