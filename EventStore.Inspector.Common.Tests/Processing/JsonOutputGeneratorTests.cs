using NUnit.Framework;
using FakeItEasy;
using EventStore.Inspector.Common.Processing;
using EventStore.Inspector.Common.Infrastructure;
using Newtonsoft.Json.Linq;

namespace EventStore.Inspector.Common.Tests.Processing
{
    [TestFixture]
    public class JsonOutputGeneratorTests
    {
        private const string AStreamId = "MyStream";
        private const string AnEventType = "MyEventType";
        private const string SomeMetadata = "any data";
        private const long AnEventNumber = 33345555L;

        private IOutputStream _outputStream;
        private IEventRecord _eventRecord;
        private JsonOutputGenerator _outputGenerator;

        [SetUp]
        public void Before()
        {
            _outputStream = A.Fake<IOutputStream>();
            _eventRecord = A.Fake<IEventRecord>();

            A.CallTo(() => _eventRecord.EventStreamId).Returns(AStreamId);
            A.CallTo(() => _eventRecord.EventNumber).Returns(AnEventNumber);
            A.CallTo(() => _eventRecord.EventType).Returns(AnEventType);
            A.CallTo(() => _eventRecord.Metadata).Returns(SomeMetadata);

            _outputGenerator = new JsonOutputGenerator(_outputStream);
        }

        [Test]
        public void OnPositiveEvent_append_json_event_record()
        {
            A.CallTo(() => _eventRecord.IsJson).Returns(true);
            A.CallTo(() => _eventRecord.Body).Returns("{}");

            _outputGenerator.OnPositiveEvent(_eventRecord);

            A.CallTo(() => _outputStream.Append(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void OnPositiveEvent_append_non_json_event_record()
        {
            A.CallTo(() => _eventRecord.IsJson).Returns(false);
            A.CallTo(() => _eventRecord.Body).Returns("xxx");

            _outputGenerator.OnPositiveEvent(_eventRecord);

            A.CallTo(() => _outputStream.Append(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
