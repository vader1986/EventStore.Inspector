using NUnit.Framework;
using EventStore.Inspector.Common.SearchFilters;

namespace EventStore.Inspector.Common.Tests.SearchFilters
{
    [TestFixture]
    public class TestFilterTests
    {
        [Test]
        public void IsMatch_true_if_data_contains_text()
        {
            var filter = new TextFilter("abc");

            Assert.That(filter.IsMatch("xxxabcxxx"));
        }

        [Test]
        public void IsMatch_false_if_data_does_not_contain_text()
        {
            var filter = new TextFilter("abc");

            Assert.That(filter.IsMatch("xxxaxbxcxxx"), Is.False);
        }
    }
}
