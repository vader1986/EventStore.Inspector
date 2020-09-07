using System.Collections.Generic;

namespace EventStore.Inspector.Common.Analysis
{
    public class AnalysisOptions
    {
        public IEnumerable<string> EventTypes { get; }

        public AnalysisOptions(IEnumerable<string> eventTypes)
        {
            EventTypes = eventTypes;
        }
    }
}
