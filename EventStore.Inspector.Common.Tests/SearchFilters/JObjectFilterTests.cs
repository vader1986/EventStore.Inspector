using NUnit.Framework;
using EventStore.Inspector.Common.SearchFilters;

namespace EventStore.Inspector.Common.Tests.SearchFilters
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void WhenPredicateIsFulfilledIsMatchReturnsTrue()
        {
            var data = "{\"name\": \"Peter\"}";

            var filter = new JObjectFilter(obj => obj.Value<string>("name") == "Peter");

            Assert.That(filter.IsMatch(data));
        }

        [Test]
        public void WhenPredicateIsNotFulfilledIsMatchReturnsFalse()
        {
            var data = "{\"name\": \"Anna\"}";

            var filter = new JObjectFilter(obj => obj.Value<string>("name") == "Peter");

            Assert.That(filter.IsMatch(data), Is.False);
        }
    }
}