using System.Linq;
using EventStore.Inspector.Common.Analysis;
using EventStore.Inspector.Common.Search;
using EventStore.Inspector.Common.Infrastructure.Throttling;

namespace EventStore.Inspector
{
    public static class OptionsTranslator
    {
        public static Common.Search.SearchOptions From(SearchOptions options)
        {
            var propertyFilters = options.SearchProperty.Select(p => p.Split(':')).Select(s => new JsonPropertyFilter(s[0], s[1]));
            var textFilters = options.SearchText.Select(p => new TextFilter(p));
            var regexFilters = options.SearchRegex.Select(p => new RegexFilter(p));

            var allFilters = propertyFilters.Concat<ISearchFilter>(textFilters).Concat(regexFilters);

            return new Common.Search.SearchOptions(
                allFilters,
                options.Stream,
                options.OutputFormat,
                options.Aggregation);
        }

        public static Common.Analysis.AnalysisOptions From(AnalyseOptions options)
        {
            return new Common.Analysis.AnalysisOptions(options.EventTypes, Window.Events);
        }

        public static Common.Infrastructure.ConnectionOptions ConnectionOptionsFrom(ConnectionOptions options)
        {
            return new Common.Infrastructure.ConnectionOptions(
                options.ConnectionString,
                options.ReadForward,
                options.BatchSize,
                new ThrottleOptions(
                    options.BatchMode,
                    options.SleepIntervalMilliSeconds));
        }
    }
}
