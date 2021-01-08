using System;
using System.Threading.Tasks;
using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Search
{
    public class Search
    {
        private readonly IConnectionWrapper _connectionWrapper;
        private readonly IOutputStream _outputStream;

        public Search(IConnectionWrapper connectionWrapper, IOutputStream outputStream)
        {
            _connectionWrapper = connectionWrapper;
            _outputStream = outputStream;
        }

        public async Task For(SearchOptions options)
        {
            IEvaluationListener OutputGenerator(IOutputStream stream)
            {
                return options.OutputFormat switch
                {
                    OutputFormat.Text => new TextOutputGenerator(stream),
                    OutputFormat.Json => new JsonOutputGenerator(stream),
                    _ => throw new ArgumentOutOfRangeException(
                        nameof(stream), $"{options.OutputFormat} not supported")
                };
            }

            var output = OutputGenerator(_outputStream);

            var aggregatedFilter = new AggregatedSearchFilter(options.AggregationMethod, options.SearchFilters);
            var evaluator = new JsonEventEvaluator(aggregatedFilter, output);

            await _connectionWrapper.ConnectToStreamAsync(options.Stream, evaluator.Evaluate);
        }
    }
}
