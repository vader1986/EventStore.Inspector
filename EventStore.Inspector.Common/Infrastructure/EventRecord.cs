using System.Text;
using EventStore.ClientAPI;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class EventRecord : IEventRecord
    {
        private readonly ResolvedEvent _resolvedEvent;

        private EventRecord(ResolvedEvent resolvedEvent)
        {
            _resolvedEvent = resolvedEvent;
        }

        public static EventRecord From(ResolvedEvent resolvedEvent)
        {
            return new EventRecord(resolvedEvent);
        }

        public bool IsValid => _resolvedEvent.Event?.Data != null;

        public bool IsMetadata => false; // ToDo

        public bool IsJson => IsValid && _resolvedEvent.Event.IsJson;

        public string Body => Encoding.UTF8.GetString(_resolvedEvent.Event.Data);

        public string? Metadata => _resolvedEvent.Event.Metadata == null ? null : Encoding.UTF8.GetString(_resolvedEvent.Event.Metadata);

        public string EventStreamId => _resolvedEvent.Event.EventStreamId;

        public string EventType => _resolvedEvent.Event.EventType;

        public long EventNumber => _resolvedEvent.Event.EventNumber;
    }
}
