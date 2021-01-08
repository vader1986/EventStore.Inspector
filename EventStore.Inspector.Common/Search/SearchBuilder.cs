using EventStore.Inspector.Common.Infrastructure;
using EventStore.Inspector.Common.Infrastructure.Throttling;

namespace EventStore.Inspector.Common.Search
{
    public class SearchBuilder
    {
        private readonly ConnectionOptions _connectionOptions;
        private IOutputStream _outputStream = new ConsoleOutputStream();
        private IThrottleFactory? _throttleFactory;

        private SearchBuilder(ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
        }

        public static SearchBuilder From(ConnectionOptions connectionOptions)
        {
            return new SearchBuilder(connectionOptions);
        }

        public SearchBuilder WithOutputStream(IOutputStream outputStream)
        {
            _outputStream = outputStream;
            return this;
        }

        public SearchBuilder WithThrottleFactory(IThrottleFactory throttleFactory)
        {
            _throttleFactory = throttleFactory;
            return this;
        }

        public Search Build()
        {
            return new Search(new ConnectionWrapper(_connectionOptions, _throttleFactory), _outputStream);
        }
    }
}
