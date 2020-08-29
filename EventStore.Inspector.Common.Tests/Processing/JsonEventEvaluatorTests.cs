using EventStore.Inspector.Common.Infrastructure;
using EventStore.Inspector.Common.Processing;
using EventStore.Inspector.Common.SearchFilters;
using FakeItEasy;
using NUnit.Framework;

namespace EventStore.Inspector.Common.Tests.Processing
{
    [TestFixture]
    public class JsonEventEvaluatorTests
    {
        private ISearchFilter _filter;
        private IEvaluationListener _listener;
        private JsonEventEvaluator _evaluator;

        [SetUp]
        public void Before()
        {
            _filter = A.Fake<ISearchFilter>();
            _listener = A.Fake<IEvaluationListener>();
            _evaluator = new JsonEventEvaluator(_filter, _listener);
        }

        [Test]
        public void Evaluate_on_json_event_notify_listener()
        {
            A.CallTo(() => _filter.IsMatch(A<string>.Ignored)).Returns(true);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(false);

            _evaluator.Evaluate(record);

            A.CallTo(() => _listener.OnPositiveEvent(record)).MustHaveHappened();
        }

        [Test]
        public void Evaluate_on_mismatch_no_notification()
        {
            A.CallTo(() => _filter.IsMatch(A<string>.Ignored)).Returns(false);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(false);

            _evaluator.Evaluate(record);

            A.CallTo(() => _listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }

        [Test]
        public void Evaluate_on_invalid_event_no_notification()
        {
            A.CallTo(() => _filter.IsMatch(A<string>.Ignored)).Returns(true);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(false);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(false);

            _evaluator.Evaluate(record);

            A.CallTo(() => _listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }

        [Test]
        public void Evaluate_on_non_json_event_no_notification()
        {
            A.CallTo(() => _filter.IsMatch(A<string>.Ignored)).Returns(true);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(false);
            A.CallTo(() => record.IsMetadata).Returns(false);

            _evaluator.Evaluate(record);

            A.CallTo(() => _listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }

        [Test]
        public void Evaluate_on_metadata_event_no_notification()
        {
            A.CallTo(() => _filter.IsMatch(A<string>.Ignored)).Returns(true);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(true);

            _evaluator.Evaluate(record);

            A.CallTo(() => _listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }
    }
}
