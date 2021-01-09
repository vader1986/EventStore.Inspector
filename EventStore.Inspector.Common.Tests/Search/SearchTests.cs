using System.Threading.Tasks;
using EventStore.Inspector.Common.Infrastructure;
using EventStore.Inspector.Common.Infrastructure.Throttling;
using EventStore.Inspector.Common.Search;
using EventStore.Inspector.Testing;
using EventStore.Inspector.Testing.Events;
using FakeItEasy;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Search
{
    [TestFixture]
    public class SearchTests
    {
        private static readonly EventWrapperBuilder AnEvent = new EventWrapperBuilder().WithEventData(new { id = "ABC-123", text = "my text" });

        private IOutputStream _outputStream;
        private ThrottleOptions _throttleOptions;
        private SearchOptions _searchOptions;

        [SetUp]
        public void Before()
        {
            _outputStream = A.Fake<IOutputStream>();
            _searchOptions = new SearchOptions(new[] { new TextFilter("my text") }, AnEvent.Build().Stream, OutputFormat.Text, AggregationMethod.And);
            _throttleOptions = new ThrottleOptions(BatchMode.Continue, 0);
        }

        [Test]
        public async Task For_search_options_matching_an_event_writes_result_to_output_stream()
        {
            var @event = AnEvent.Build();
            using var eventStore = await EventStoreRunner.Start(@event.Stream);

            await eventStore.Write(@event);

            var options = new ConnectionOptions(eventStore.Connection, true, 50, _throttleOptions);

            await SearchBuilder
                .From(options)
                .WithOutputStream(_outputStream)
                .Build()
                .For(_searchOptions);

            A.CallTo(() => _outputStream.Append(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task For_search_options_not_matching_any_events_does_not_write_to_output_stream()
        {
            var @event = AnEvent.WithEventData(new { ignore = "this" }).Build();
            using var eventStore = await EventStoreRunner.Start(@event.Stream);

            await eventStore.Write(@event);

            var options = new ConnectionOptions(eventStore.Connection, true, 50, _throttleOptions);

            await SearchBuilder
                .From(options)
                .WithOutputStream(_outputStream)
                .Build()
                .For(_searchOptions);

            A.CallTo(() => _outputStream.Append(A<string>.Ignored)).MustNotHaveHappened();
        }
    }
}
