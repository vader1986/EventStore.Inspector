using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EventStore.Inspector.Testing.Bus
{
    public class Bus<T>
    {
        private const int DefaultTimeoutMs = 5000;

        private readonly BlockingCollection<T> _bus = new BlockingCollection<T>();
        private readonly TimeSpan _timeout;

        public Bus(TimeSpan? timeout = default)
        {
            _timeout = timeout ?? TimeSpan.FromMilliseconds(DefaultTimeoutMs);
        }

        public void Add(T item)
        {
            _bus.Add(item);
        }

        public void WaitForNext(Predicate<T> predicate)
        {
            if (_bus.TryTake(out var item, _timeout))
            {
                if (!predicate(item))
                {
                    throw new ItemNotFoundException<T>();
                }
            }
            else
            {
                throw new ItemNotFoundException<T>(_timeout);
            }
        }
    }
}
