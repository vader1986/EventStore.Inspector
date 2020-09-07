using EventStore.Inspector.Common.Search;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Search
{
    [TestFixture]
    public class RegexFilterTests
    {
        [Test]
        public void IsMatch_true_if_data_matches_regex()
        {
            var filter = new RegexFilter("xxx");

            Assert.That(filter.IsMatch("xxx"), Is.True);
        }

        [Test]
        public void IsMatch_false_if_data_not_matching_regex()
        {
            var filter = new RegexFilter("xxx");

            Assert.That(filter.IsMatch("yyy"), Is.False);
        }
    }
}
