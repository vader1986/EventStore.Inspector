using System;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.Inspector.Common.Search
{
    public class AggregatedSearchFilter : ISearchFilter
    {
        private readonly AggregationMethod _aggregationMethod;
        private readonly IEnumerable<ISearchFilter> _searchFilters;

        public AggregatedSearchFilter(AggregationMethod aggregationMethod, IEnumerable<ISearchFilter> searchFilters)
        {
            _aggregationMethod = aggregationMethod;
            _searchFilters = searchFilters;
        }

        public bool IsMatch(string data)
        {
            return _aggregationMethod switch
            {
                AggregationMethod.And => _searchFilters.All(f => f.IsMatch(data)),
                AggregationMethod.Or => _searchFilters.Any(f => f.IsMatch(data)),
                _ => throw new InvalidOperationException(
                    $"Aggregation method {_aggregationMethod} is not supported."),
            };
        }
    }
}
