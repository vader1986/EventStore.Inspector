using System;
using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Processing
{
    public class TextOutputGenerator : IEvaluationListener
    {
        public void OnPositiveEvent(IEventRecord record)
        {
            Console.WriteLine($"{record.EventStreamId}/{record.EventNumber} [{record.EventType}]");
        }
    }
}
