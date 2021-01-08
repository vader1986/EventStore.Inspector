namespace EventStore.Inspector.Testing.Events
{
    public interface ISerializer
    {
        string Serialize(object data);
    }
}
