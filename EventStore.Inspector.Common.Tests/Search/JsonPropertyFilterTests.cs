using EventStore.Inspector.Common.Search;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Search
{
    [TestFixture]
    public class JsonPropertyFilterTests
    {
        [Test]
        public void IsMatch_true_if_data_contains_key_value()
        {
            var filter = new JsonPropertyFilter("key", "1");
            var data = new JObject(new JProperty("key", 1)).ToString();

            Assert.That(filter.IsMatch(data), Is.True);
        }

        [Test]
        public void IsMatch_true_if_data_contains_nested_key_value()
        {
            var filter = new JsonPropertyFilter("key", "1");
            var data = new JObject(
                new JProperty("data", new JObject(
                    new JProperty("key", 1)))).ToString();

            Assert.That(filter.IsMatch(data), Is.True);
        }

        [Test]
        public void IsMatch_false_if_data_contains_only_key()
        {
            var filter = new JsonPropertyFilter("key", "1");
            var data = new JObject(new JProperty("key", 2)).ToString();

            Assert.That(filter.IsMatch(data), Is.False);
        }

        [Test]
        public void IsMatch_false_if_data_does_not_contain_key()
        {
            var filter = new JsonPropertyFilter("key", "1");
            var data = new JObject(new JProperty("x", 2)).ToString();

            Assert.That(filter.IsMatch(data), Is.False);
        }
    }
}
