using System;

namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    public class ThrottleFactory : IThrottleFactory
    {
        public IThrottle Create(ThrottleOptions options)
        {
            switch (options.BatchMode)
            {
                case BatchMode.AwaitUserInput:
                    return new WaitForUserInput();

                case BatchMode.Continue:
                    return new NoOp();

                case BatchMode.Sleep:
                    return new SleepForAWhile(options.SleepIntervalMilliSeconds);

                default:
                    throw new InvalidOperationException($"{options.BatchMode} is not supported");
            }
        }
    }
}
