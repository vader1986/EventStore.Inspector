using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Search
{
    public class JsonEventEvaluator
    {
        private readonly IEvaluationListener _evaluationListener;
        private readonly ISearchFilter _searchFilter;

        public JsonEventEvaluator(ISearchFilter searchFilter, IEvaluationListener evaluationListener)
        {
            _evaluationListener = evaluationListener;
            _searchFilter = searchFilter;
        }

        public void Evaluate(IEventRecord record)
        {
            if (record.IsValid && record.IsJson && !record.IsMetadata)
            {
                if (_searchFilter.IsMatch(record.Body))
                {
                    _evaluationListener.OnPositiveEvent(record);
                }
            }
        }
    }
}
