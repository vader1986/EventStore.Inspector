using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Search
{
    public interface IEvaluationListener
    {
        void OnPositiveEvent(IEventRecord record);
    }
}
