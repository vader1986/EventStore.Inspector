namespace EventStore.Inspector.Common.Infrastructure
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; }

        /*
         * ToDo:
         * 1) batch size
         * 2) delay after each batch
         * 3) await user input after batch
         * 4) read forwards or backwards
         */

        public ConnectionOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
