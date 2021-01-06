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

        /// <summary>
        /// Creates a new <see cref="IEventStoreConnection"/> instance for the specified
        /// connection string. 
        /// </summary>
        /// <param name="connectionString">Connection string pointing to a running
        /// EventStoreDB instance</param>
        /// <returns>A new <see cref="IEventStoreConnection"/> instance.</returns>
        public static ValueTask<IEventStoreConnection> Create(string connectionString)
        {
            return new ValueTask<IEventStoreConnection>(CreateConnection(connectionString));
        }
    }
}
