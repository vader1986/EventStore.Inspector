using System;
using System.Collections.Generic;
using EventStore.Inspector.Common.SearchFilters;
using EventStore.Inspector.Common.Validation;

namespace EventStore.Inspector.Common
{
    public class Options
    {
        public string Stream { get; }
        public OutputFormat OutputFormat { get; }
        public AggregationMethod AggregationMethod { get; }
        public IEnumerable<ISearchFilter> SearchFilters { get; }

        public Options(IEnumerable<ISearchFilter> searchFilters, string stream, OutputFormat outputFormat, AggregationMethod aggregationMethod)
        {
            Stream = new StreamNameValidator().Validate(stream);
            OutputFormat = outputFormat;
            AggregationMethod = aggregationMethod;
            SearchFilters = searchFilters ?? throw new ArgumentNullException(nameof(searchFilters));
        }
    }
}
