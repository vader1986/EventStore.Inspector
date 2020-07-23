using System;
using System.Threading.Tasks;

namespace EventStore.Inspector.Common.Infrastructure
{
    public interface IConnectionWrapper
    {
        Task ConnectToStreamAsync(string stream, Action<IEventRecord> onEventRead);

        /*
         * ToDo
         * 1) maximum number of events to read
         * 2) cancellation token
         * 3) optional timeout
         */
    }
}
