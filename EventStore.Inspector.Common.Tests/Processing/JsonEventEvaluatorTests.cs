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
        [Test]
        public void Evaluate_on_json_event_notify_listener()
        {
            var filter = A.Fake<ISearchFilter>();
            var listener = A.Fake<IEvaluationListener>();

            A.CallTo(() => filter.IsMatch(A<string>.Ignored)).Returns(true);

            var evaluator = new JsonEventEvaluator(filter, listener);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(false);

            evaluator.Evaluate(record);

            A.CallTo(() => listener.OnPositiveEvent(record)).MustHaveHappened();
        }

        [Test]
        public void Evaluate_on_mismatch_no_notification()
        {
            var filter = A.Fake<ISearchFilter>();
            var listener = A.Fake<IEvaluationListener>();

            A.CallTo(() => filter.IsMatch(A<string>.Ignored)).Returns(false);

            var evaluator = new JsonEventEvaluator(filter, listener);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(false);

            evaluator.Evaluate(record);

            A.CallTo(() => listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }

        [Test]
        public void Evaluate_on_invalid_event_no_notification()
        {
            var filter = A.Fake<ISearchFilter>();
            var listener = A.Fake<IEvaluationListener>();

            A.CallTo(() => filter.IsMatch(A<string>.Ignored)).Returns(true);

            var evaluator = new JsonEventEvaluator(filter, listener);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(false);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(false);

            evaluator.Evaluate(record);

            A.CallTo(() => listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }

        [Test]
        public void Evaluate_on_non_json_event_no_notification()
        {
            var filter = A.Fake<ISearchFilter>();
            var listener = A.Fake<IEvaluationListener>();

            A.CallTo(() => filter.IsMatch(A<string>.Ignored)).Returns(true);

            var evaluator = new JsonEventEvaluator(filter, listener);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(false);
            A.CallTo(() => record.IsMetadata).Returns(false);

            evaluator.Evaluate(record);

            A.CallTo(() => listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }

        [Test]
        public void Evaluate_on_metadata_event_no_notification()
        {
            var filter = A.Fake<ISearchFilter>();
            var listener = A.Fake<IEvaluationListener>();

            A.CallTo(() => filter.IsMatch(A<string>.Ignored)).Returns(true);

            var evaluator = new JsonEventEvaluator(filter, listener);

            var record = A.Fake<IEventRecord>();

            A.CallTo(() => record.IsValid).Returns(true);
            A.CallTo(() => record.IsJson).Returns(true);
            A.CallTo(() => record.IsMetadata).Returns(true);

            evaluator.Evaluate(record);

            A.CallTo(() => listener.OnPositiveEvent(record)).MustNotHaveHappened();
        }
    }
}
