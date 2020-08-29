using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Processing
{
    public class TextOutputGenerator : IEvaluationListener
    {
        private readonly IOutputStream _outputStream;

        public TextOutputGenerator(IOutputStream outputStream)
        {
            _outputStream = outputStream;
        }

        public void OnPositiveEvent(IEventRecord record)
        {
            _outputStream.Append($"{record.EventStreamId}/{record.EventNumber} [{record.EventType}]");
        }
    }
}
