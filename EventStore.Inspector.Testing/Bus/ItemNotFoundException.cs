using System;
namespace EventStore.Inspector.Testing.Bus
{
    public class ItemNotFoundException<T> : Exception
    {
        private static string GetMessage(TimeSpan timeout)
        {
            return $"{typeof(T).Name} not found on the bus after {timeout}";
        }

        public ItemNotFoundException(TimeSpan timeout, Exception innerException = default) : base(GetMessage(timeout), innerException)
        {

        }
    }
}
