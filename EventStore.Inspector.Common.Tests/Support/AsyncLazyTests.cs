using System.Threading.Tasks;
using EventStore.Inspector.Common.Support;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Support
{
    [TestFixture]
    public class AsyncLazyTests
    {
        [Test]
        public async Task Await_AsyncLazy_object_from_sync_factory_multiple_time_returns_same_value()
        {
            var lazyValue = new AsyncLazy<object>(() => new object());

            var value1 = await lazyValue;
            var value2 = await lazyValue;

            Assert.That(value1 == value2);
        }

        [Test]
        public async Task Await_AsyncLazy_object_from_async_factory_multiple_time_returns_same_value()
        {
            var lazyValue = new AsyncLazy<object>(() => new ValueTask<object>(new object()));

            var value1 = await lazyValue;
            var value2 = await lazyValue;

            Assert.That(value1 == value2);
        }
    }
}
