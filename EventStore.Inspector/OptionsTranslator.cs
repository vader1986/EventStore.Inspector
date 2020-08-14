using System;
using System.Linq;
using EventStore.Inspector.Common;
using EventStore.Inspector.Common.Infrastructure;
using EventStore.Inspector.Common.SearchFilters;

namespace EventStore.Inspector
{
    public static class OptionsTranslator
    {
        public static Options From(CommandLineOptions options)
        {
            var propertyFilters = options.SearchProperty.Select(p => p.Split(':')).Select(s => new JsonPropertyFilter(s[0], s[1]));
            var textFilters = options.SearchText.Select(p => new TextFilter(p));
            var regexFilters = options.SearchRegex.Select(p => new RegexFilter(p));

            var allFilters = propertyFilters.Concat<ISearchFilter>(textFilters).Concat(regexFilters);

            return new Options(
                allFilters,
                options.Stream,
                options.OutputFormat,
                options.Aggregation);
        }

        public static ConnectionOptions ConnectionOptionsFrom(CommandLineOptions options)
        {
            TimeSpan? ConvertSleepInterval()
            {
                return options.SleepIntervalInSeconds > 0 ? TimeSpan.FromMilliseconds(options.SleepIntervalInSeconds) : default;
            }

            return new ConnectionOptions(
                options.ConnectionString,
                options.ReadForward,
                options.BatchSize,
                options.BatchMode,
                ConvertSleepInterval());
        }
    }
}
