using System.Threading.Tasks;
using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Search
{
    public class Search
    {
        private readonly IConnectionWrapper _connectionWrapper;

        private Search(ConnectionOptions connectionOptions)
        {
            _connectionWrapper = new ConnectionWrapper(connectionOptions);
        }

        public static Search Create(ConnectionOptions connectionOptions)
        {
            return new Search(connectionOptions);
        }

        public async Task For(SearchOptions options)
        {
            IEvaluationListener OutputGenerator(IOutputStream stream)
            {
                switch (options.OutputFormat)
                {
                    case OutputFormat.Text:
                        return new TextOutputGenerator(stream);
                    case OutputFormat.Json:
                        return new JsonOutputGenerator(stream);
                }

                return default;
            }

            var outputStream = new ConsoleOutputStream();
            var output = OutputGenerator(outputStream);

            var aggregatedFilter = new AggregatedSearchFilter(options.AggregationMethod, options.SearchFilters);
            var evaluator = new JsonEventEvaluator(aggregatedFilter, output);

            await _connectionWrapper.ConnectToStreamAsync(options.Stream, evaluator.Evaluate);
        }
    }
}
