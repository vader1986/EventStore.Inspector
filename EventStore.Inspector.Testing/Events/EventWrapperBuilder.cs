namespace EventStore.Inspector.Testing.Events
{
    public class EventWrapperBuilder
    {
        public const string DefaultEventType = nameof(DefaultEventType);
        public const string DefaultStream = nameof(DefaultStream);

        private ISerializer _serializer = new JsonSerializer();
        private object _eventData = new object();
        private object _eventMetadata;
        private string _eventType = DefaultEventType;
        private string _stream = DefaultStream;

        public EventWrapperBuilder WithSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            return this;
        }

        public EventWrapperBuilder WithEventData(object eventData)
        {
            _eventData = eventData;
            return this;
        }

        public EventWrapperBuilder WithEventMetadata(object eventMetadata)
        {
            _eventMetadata = eventMetadata;
            return this;
        }

        public EventWrapperBuilder WithEventType(string eventType)
        {
            _eventType = eventType;
            return this;
        }

        public EventWrapperBuilder WithStream(string stream)
        {
            _stream = stream;
            return this;
        }

        public EventWrapper Build()
        {
            var body = _serializer.Serialize(_eventData);
            var metadata = _eventMetadata != null ? _serializer.Serialize(_eventMetadata) : null;

            return new EventWrapper(body, _stream, _eventType, metadata, _serializer is JsonSerializer);
        }
    }
}
