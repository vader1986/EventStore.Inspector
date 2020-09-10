using System.Linq;
using Newtonsoft.Json.Linq;

namespace EventStore.Inspector.Common.Search
{
    public class JsonPropertyFilter : ISearchFilter
    {
        private readonly string _key;
        private readonly string _value;

        public JsonPropertyFilter(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public bool IsMatch(string data)
        {
            bool IsMatchingObject(JToken token)
            {
                if (!(token is JObject obj))
                {
                    return false;
                }

                var val = obj[_key];

                return val != null && val.ToString() == _value;
            }

            var root = JObject.Parse(data);

            return IsMatchingObject(root) || root.Descendants().Any(IsMatchingObject);
        }
    }
}
