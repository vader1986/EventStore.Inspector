using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStore.Inspector.Testing.Events
{
    public class EventStoreAppender
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreAppender(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task Append(EventWrapper @event)
        {
            var body = Encoding.UTF8.GetBytes(@event.Body);
            var metadata = @event.Metadata != null ? Encoding.UTF8.GetBytes(@event.Metadata) : new byte[0];
            var data = new EventData(@event.Id, @event.EventType, @event.IsJson, body, metadata);

            await _connection.AppendToStreamAsync(@event.Stream, ExpectedVersion.Any, data);
        }
    }
}
