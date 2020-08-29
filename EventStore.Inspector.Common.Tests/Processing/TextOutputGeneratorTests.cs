using NUnit.Framework;
using FakeItEasy;
using EventStore.Inspector.Common.Processing;
using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Tests.Processing
{
    [TestFixture]
    public class TextOutputGeneratorTests
    {
        [Test]
        public void OnPositiveEvent_append_event_record()
        {
            const string expectedStreamId = "Test-Stream";
            const string expectedEventType = "Test.Type";
            const long expectedEventNumber = 123L;
            var expectedOutput = $"{expectedStreamId}/{expectedEventNumber} [{expectedEventType}]";

            var outputStream = A.Fake<IOutputStream>();
            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.EventStreamId).Returns(expectedStreamId);
            A.CallTo(() => record.EventNumber).Returns(expectedEventNumber);
            A.CallTo(() => record.EventType).Returns(expectedEventType);

            var outputGenerator = new TextOutputGenerator(outputStream);

            outputGenerator.OnPositiveEvent(record);

            A.CallTo(() => outputStream.Append(expectedOutput)).MustHaveHappenedOnceExactly();
        }
    }
}
