using System.Threading.Tasks;

namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    /// <summary>
    /// Representation of a throttling mechanism. 
    /// </summary>
    public interface IThrottle
    {
        /// <summary>
        /// Creates a task to throttle a process for some time.
        /// </summary>
        /// <returns>A task which is completed when the process has been throttled for the desired amount of time.</returns>
        Task Throttle();
    }
}
