﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.Inspector.Common.SearchFilters
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
            switch (_aggregationMethod)
            {
                case AggregationMethod.And:
                    return _searchFilters.All(f => f.IsMatch(data));

                case AggregationMethod.Or:
                    return _searchFilters.Any(f => f.IsMatch(data));

                default:
                    throw new InvalidOperationException($"Aggregation method {_aggregationMethod} is not supported.");
            }
        }
    }
}
