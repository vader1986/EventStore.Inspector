using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Processing
{
    public interface IEventEvaluator
    {
        void Evaluate(IEventRecord record);
    }
}
