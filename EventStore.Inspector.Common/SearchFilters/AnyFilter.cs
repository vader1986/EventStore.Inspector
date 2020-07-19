namespace EventStore.Inspector.Common.SearchFilters
{
    public class AnyFilter : ISearchFilter
    {
        public bool IsMatch(string data)
        {
            return true;
        }
    }
}
