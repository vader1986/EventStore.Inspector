namespace EventStore.Inspector.Common.SearchFilters
{
    public class TextFilter : ISearchFilter
    {
        private readonly string _searchPattern;

        public TextFilter(string searchPattern)
        {
            _searchPattern = searchPattern;
        }

        public bool IsMatch(string data)
        {
            return data.Contains(_searchPattern);
        }
    }
}
