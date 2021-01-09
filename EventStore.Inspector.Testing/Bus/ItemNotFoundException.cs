using System;
namespace EventStore.Inspector.Testing.Bus
{
    public class ItemNotFoundException<T> : Exception
    {
        private static string GetMessage(TimeSpan timeout)
        {
            return $"{typeof(T).Name} not found on the bus after {(int)timeout.TotalSeconds}s";
        }

        private static string GetMessage()
        {
            return $"{typeof(T).Name} does not fulfill predicate.";
        }

        public ItemNotFoundException(TimeSpan timeout, Exception innerException = default) : base(GetMessage(timeout), innerException)
        {

        }

        public ItemNotFoundException(Exception innerException = default) : base(GetMessage(), innerException)
        {

        }
    }
}
