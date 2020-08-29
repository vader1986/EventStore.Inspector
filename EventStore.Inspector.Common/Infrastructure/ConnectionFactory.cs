using System;
using System.Data.Common;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionFactory
    {
        private static async Task<IEventStoreConnection> CreateConnection(string connectionString)
        {
            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            var uri = new Uri(builder["ConnectTo"].ToString());
            var settings = ConnectionString.GetConnectionSettings(connectionString);
            var connection = EventStoreConnection.Create(settings, uri);

            await connection.ConnectAsync();

            return connection;
        }

        public static Lazy<Task<IEventStoreConnection>> Create(string connectionString)
        {
            return new Lazy<Task<IEventStoreConnection>>(() => CreateConnection(connectionString));
        }
    }
}
