using System.Text.RegularExpressions;

namespace EventStore.Inspector.Common.Search
{
    public class RegexFilter : ISearchFilter
    {
        private readonly string _regex;

        public RegexFilter(string regex)
        {
            _regex = regex;
        }

        public bool IsMatch(string data)
        {
            return Regex.IsMatch(data, _regex);
        }
    }
}
