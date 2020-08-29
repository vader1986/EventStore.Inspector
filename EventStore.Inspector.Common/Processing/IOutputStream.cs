namespace EventStore.Inspector.Common.Processing
{
    public interface IOutputStream
    {
        void Append(string text);
    }
}
