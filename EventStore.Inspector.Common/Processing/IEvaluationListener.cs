using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Processing
{
    public interface IEvaluationListener
    {
        void OnPositiveEvent(IEventRecord record);
    }
}
