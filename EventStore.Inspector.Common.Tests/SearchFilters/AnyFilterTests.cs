using EventStore.Inspector.Common.SearchFilters;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.SearchFilters
{
    [TestFixture]
    public class AnyFilterTests
    {
        [Test]
        public void OnAnyInputIsMatchReturnsTrue()
        {
            var filter = new AnyFilter();

            Assert.That(filter.IsMatch("anything really"));
        }
    }
}
