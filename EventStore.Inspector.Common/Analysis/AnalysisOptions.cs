using System;
using System.Collections.Generic;

namespace EventStore.Inspector.Common.Analysis
{
    public class AnalysisOptions
    {
        public IEnumerable<string> EventTypes { get; }
        public Window Window { get; }
        public TimeSpan TimeFrame { get; }

        public AnalysisOptions(IEnumerable<string> eventTypes, Window window)
        {
            EventTypes = eventTypes;
            Window = window;
        }
    }
}
