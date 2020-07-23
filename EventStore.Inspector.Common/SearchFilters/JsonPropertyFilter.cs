using System.Linq;
using Newtonsoft.Json.Linq;

namespace EventStore.Inspector.Common.SearchFilters
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
            var root = JObject.Parse(data);

            if (root[_key] != null && root[_key].ToString() == _value)
            {
                return true;
            }

            return root
                .Descendants()
                .Where(t => t is JObject)
                .Where(t => t[_key] != null)
                .Any(t => t[_key].ToString() == _value);
        }
    }
}
