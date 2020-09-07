using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Search
{
    public interface IEventEvaluator
    {
        void Evaluate(IEventRecord record);
    }
}
