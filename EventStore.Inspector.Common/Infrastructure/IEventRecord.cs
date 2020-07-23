namespace EventStore.Inspector.Common.Infrastructure
{
    public interface IEventRecord
    {
        bool IsValid { get; }
        bool IsMetadata { get; }
        bool IsJson { get; }

        string EventStreamId { get; }
        string EventType { get; }
        long EventNumber { get; }

        string Body { get; }
        string Metadata { get; }
    }
}
