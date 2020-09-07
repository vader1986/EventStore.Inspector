using System;
using System.Collections.Generic;

namespace EventStore.Inspector.Common.Search
{
    public class SearchOptions
    {
        public string Stream { get; }
        public OutputFormat OutputFormat { get; }
        public AggregationMethod AggregationMethod { get; }
        public IEnumerable<ISearchFilter> SearchFilters { get; }

        public SearchOptions(IEnumerable<ISearchFilter> searchFilters, string stream, OutputFormat outputFormat, AggregationMethod aggregationMethod)
        {
            Stream = stream;
            OutputFormat = outputFormat;
            AggregationMethod = aggregationMethod;
            SearchFilters = searchFilters ?? throw new ArgumentNullException(nameof(searchFilters));
        }
    }
}
