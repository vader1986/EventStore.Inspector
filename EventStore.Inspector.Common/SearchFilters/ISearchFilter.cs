namespace EventStore.Inspector.Common.SearchFilters
{
    public interface ISearchFilter
    {
        bool IsMatch(string data);
    }
}
