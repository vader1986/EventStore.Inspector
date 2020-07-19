using NUnit.Framework;
using EventStore.Inspector.Common.SearchFilters;

namespace EventStore.Inspector.Common.Tests.SearchFilters
{
    [TestFixture]
    public class TestFilterTests
    {
        [Test]
        public void WhenDataContainsSearchPatternIsMatchReturnsTrue()
        {
            var filter = new TextFilter("abc");

            Assert.That(filter.IsMatch("xxxabcxxx"));
        }

        [Test]
        public void WhenDataDoesNotContainSearchPatternIsMatchReturnsFalse()
        {
            var filter = new TextFilter("abc");

            Assert.That(filter.IsMatch("xxxaxbxcxxx"), Is.False);
        }
    }
}
