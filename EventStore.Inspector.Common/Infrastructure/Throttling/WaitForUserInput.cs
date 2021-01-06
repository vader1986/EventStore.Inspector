using System;
using System.Threading.Tasks;

namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    /// <summary>
    /// Throttles a process by waiting for any (arbitrary) user input.
    /// </summary>
    public class WaitForUserInput : IThrottle
    {
        public Task Throttle()
        {
            return Console.In.ReadLineAsync();
        }
    }
}
