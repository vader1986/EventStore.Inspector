using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EventStore.Inspector.Common.Support
{
    /// <summary>
    /// Credits for this class are going to Stephen Toub.
    /// <see cref="https://devblogs.microsoft.com/pfxteam/asynclazyt/"/>
    /// </summary>
    public class AsyncLazy<T> : Lazy<Task<T>> where T : class
    {
        public AsyncLazy(Func<T> valueFactory)
            : base(() => Task.Factory.StartNew(valueFactory))
        { }

        public AsyncLazy(Func<Task<T>> taskFactory)
            : base(() => Task.Factory.StartNew(() => taskFactory()).Unwrap())
        { }

        public AsyncLazy(Func<ValueTask<T>> taskFactory)
            : base(() => Task.Factory.StartNew(() => taskFactory().AsTask()).Unwrap())
        { }

        public TaskAwaiter<T> GetAwaiter() { return Value.GetAwaiter(); }
    }
}
