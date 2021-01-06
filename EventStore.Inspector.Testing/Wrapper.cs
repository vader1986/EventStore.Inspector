using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Embedded;

namespace EventStore.Inspector.Testing
{
    public class Wrapper
    {
        public async Task<IEventStoreConnection> CreateLocalEventStoreInstance()
        {
            var node = EmbeddedVNodeBuilder
                .AsSingleNode()
                .OnDefaultEndpoints()
                .RunProjections(Common.Options.ProjectionType.All)
                .RunInMemory()
                .Build();

            await node.StartAsync(true);

            return EmbeddedEventStoreConnection.Create(node);
        }
    }
}
