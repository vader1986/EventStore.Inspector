using Newtonsoft.Json;

namespace EventStore.Inspector.Testing.Events
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
