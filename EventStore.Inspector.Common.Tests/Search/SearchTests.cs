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
        private IOutputStream _outputStream;

        [SetUp]
        public void Before()
        {
            _outputStream = A.Fake<IOutputStream>();
        }

        [Test]
        public async Task For_existing_event_writes_result_to_output_stream()
        {
            var wrapper = new EventWrapperBuilder()
                .WithEventData(new { id = "ABC-123", text = "my text" })
                .Build();

            using var eventStore = await EventStoreRunner.Start(wrapper.Stream);

            await eventStore.Write(wrapper);

            var throttleOptions = new ThrottleOptions(BatchMode.Continue, 0);
            var options = new ConnectionOptions(eventStore.Connection, true, 50, throttleOptions);

            var filter = new ISearchFilter[] { new TextFilter("my text") };
            var searchOptions = new SearchOptions(filter, wrapper.Stream, OutputFormat.Text, AggregationMethod.And);

            await SearchBuilder
                .From(options)
                .WithOutputStream(_outputStream)
                .Build()
                .For(searchOptions);

            A.CallTo(() => _outputStream.Append(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
