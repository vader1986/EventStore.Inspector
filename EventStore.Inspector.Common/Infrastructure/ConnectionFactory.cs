using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionFactory
    {
        private static async Task<IEventStoreConnection> CreateConnection(string connectionString)
        {
            var connection = EventStoreConnection.Create(connectionString);
            await connection.ConnectAsync();
            return connection;
        }

        public static Lazy<Task<IEventStoreConnection>> Create(string connectionString)
        {
            return new Lazy<Task<IEventStoreConnection>>(() => CreateConnection(connectionString));
        }
    }
}
