using System;

namespace EventStore.Inspector.Testing.Events
{
    public class EventWrapper
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Body { get; }
        public string Stream { get; }
        public string EventType { get; }
        public string Metadata { get; }
        public bool IsJson { get; }

        public EventWrapper(string body, string stream, string eventType, string metadata = default, bool isJson = true)
        {
            Body = body;
            Stream = stream;
            EventType = eventType;
            Metadata = metadata;
            IsJson = true;
        }
    }
}
