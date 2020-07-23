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

        public bool IsValid
        {
            get
            {
                return _resolvedEvent.Event?.Data != null;
            }
        }

        public bool IsMetadata
        {
            get
            {
                return false; // ToDo
            }
        }
        
        public bool IsJson
        {
            get
            {
                return IsValid && _resolvedEvent.Event.IsJson;
            }
        }

        public string Body
        {
            get
            {
                return Encoding.UTF8.GetString(_resolvedEvent.Event.Data);
            }
        }

        public string Metadata
        {
            get
            {
                if (_resolvedEvent.Event.Metadata == null)
                {
                    return null;
                }

                return Encoding.UTF8.GetString(_resolvedEvent.Event.Metadata);
            }
        }

        public string EventStreamId
        {
            get
            {
                return _resolvedEvent.Event.EventStreamId;
            }
        }

        public string EventType
        {
            get
            {
                return _resolvedEvent.Event.EventType;
            }
        }

        public long EventNumber
        {
            get
            {
                return _resolvedEvent.Event.EventNumber;
            }
        }
    }
}
