using System.Threading.Tasks;
using EventStore.Inspector.Common.Infrastructure;

namespace EventStore.Inspector.Common.Analysis
{
    public class Analysis
    {
        private readonly IConnectionWrapper _connectionWrapper;

        private Analysis(ConnectionOptions connectionOptions)
        {
            _connectionWrapper = new ConnectionWrapper(connectionOptions);
        }

        public static Analysis Create(ConnectionOptions connectionOptions)
        {
            return new Analysis(connectionOptions);
        }

        public async Task For(AnalysisOptions options)
        {

        }
    }
}
