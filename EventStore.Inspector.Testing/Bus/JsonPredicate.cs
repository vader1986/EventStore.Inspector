using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace EventStore.Inspector.Testing.Bus
{
    public class JsonPredicate
    {
        private readonly IDictionary<string, object> _expectedKeyValues;

        public JsonPredicate(IDictionary<string, object> expectedKeyValues)
        {
            _expectedKeyValues = expectedKeyValues ?? new Dictionary<string, object>();
        }

        public static JsonPredicate Create(IDictionary<string, object> expectedKeyValues = default)
        {
            return new JsonPredicate(expectedKeyValues);
        }

        public JsonPredicate With<T>(string key, T value)
        {
            _expectedKeyValues.Add(key, value);
            return this;
        }

        public void Assert(string json)
        {
            var document = JObject.Parse(json);

            foreach (var kvp in _expectedKeyValues)
            {
                var actualValue = document.SelectToken(kvp.Key).Value<object>();
                if (actualValue != kvp.Value)
                {
                    throw new AssertionException($"Value {actualValue} for JSON key {kvp.Key} does not match expected value {kvp.Value}");
                }
            }
        }
    }
}
