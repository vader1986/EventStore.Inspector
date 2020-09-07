using EventStore.Inspector.Common.Infrastructure;
using Newtonsoft.Json.Linq;

namespace EventStore.Inspector.Common.Search
{
    public class JsonOutputGenerator : IEvaluationListener
    {
        private readonly IOutputStream _outputStream;

        public JsonOutputGenerator(IOutputStream outputStream)
        {
            _outputStream = outputStream;
        }

        public void OnPositiveEvent(IEventRecord record)
        {
            var body = record.IsJson ? (object)JObject.Parse(record.Body) : record.Body;
            var json = new JObject(
                new JProperty("stream", record.EventStreamId),
                new JProperty("eventNumber", record.EventNumber),
                new JProperty("eventType", record.EventType),
                new JProperty("body", body),
                new JProperty("metadata", record.Metadata));

            _outputStream.Append(json.ToString(Newtonsoft.Json.Formatting.None));
        }
    }
}
