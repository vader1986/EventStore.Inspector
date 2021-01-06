using System.Threading.Tasks;

namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    /// <summary>
    /// Throttle a process by sleeping in between. The <see cref="SleepForAWhile"/>
    /// mechanism will use <see cref="Task.Delay(int)"/> to throttle the execution
    /// of the process with the number of milliseconds passed to the constructor.
    /// </summary>
    public class SleepForAWhile : IThrottle
    {
        private readonly int _milliSeconds;

        /// <summary>
        /// Creates a new instance of <see cref="SleepForAWhile"/>.
        /// </summary>
        /// <param name="milliSeconds">For how long to throttle a process in [ms].</param>
        public SleepForAWhile(int milliSeconds)
        {
            _milliSeconds = milliSeconds;
        }

        public Task Throttle()
        {
            return Task.Delay(_milliSeconds);
        }
    }
}
