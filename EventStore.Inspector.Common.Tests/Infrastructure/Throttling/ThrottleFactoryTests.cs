using System;
using EventStore.Inspector.Common.Infrastructure;
using EventStore.Inspector.Common.Infrastructure.Throttling;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Infrastructure.Throttling
{
    [TestFixture]
    public class ThrottleFactoryTests
    {
        [TestCase(BatchMode.Sleep, typeof(SleepForAWhile))]
        [TestCase(BatchMode.AwaitUserInput, typeof(WaitForUserInput))]
        [TestCase(BatchMode.Continue, typeof(NoOp))]
        public void Create_for_sleep_mode(BatchMode batchMode, Type expectedType)
        {
            var factory = new ThrottleFactory();
            var options = new ThrottleOptions(batchMode, 100);

            var product = factory.Create(options);

            Assert.IsInstanceOf(expectedType, product);
        }
    }
}
