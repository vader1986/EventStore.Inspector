namespace EventStore.Inspector.Common.Search
{
    public interface ISearchFilter
    {
        bool IsMatch(string data);
    }
}
