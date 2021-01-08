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
            var finished = Task.Run(() =>
            {
                while (_bus.TryTake(out T item, -1))
                {
                    if (predicate(item))
                    {
                        break;
                    }
                }
            }).Wait(_timeout);

            if (!finished)
            {
                throw new ItemNotFoundException<T>(_timeout);
            }
        }

    }
}
