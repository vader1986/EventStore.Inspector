namespace EventStore.Inspector.Common.Infrastructure
{
    public interface IOutputStream
    {
        void Append(string text);
    }
}
