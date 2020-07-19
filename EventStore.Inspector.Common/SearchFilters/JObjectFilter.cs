using System;
using Newtonsoft.Json.Linq;

namespace EventStore.Inspector.Common.SearchFilters
{
    public class JObjectFilter : ISearchFilter
    {
        private readonly Func<JObject, bool> _predicate;

        public JObjectFilter(Func<JObject, bool> predicate)
        {
            _predicate = predicate;
        }

        public bool IsMatch(string data)
        {
            return _predicate(JObject.Parse(data));
        }
    }
}
