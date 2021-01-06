using System.Threading.Tasks;

namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    /// <summary>
    /// This throttling mechnism just returns a completed task. It represents
    /// continuous processing without any throttling. Therefore the name "NoOp"
    /// because the throttling operation is no operation.
    /// </summary>
    public class NoOp : IThrottle
    {
        public Task Throttle()
        {
            return Task.CompletedTask;
        }
    }
}
