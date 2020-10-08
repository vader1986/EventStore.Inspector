using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace EventStore.Inspector.Common.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<IList<T>> SlidingWindow<T>(this IObservable<T> source, int size)
        {
            var feed = source.Publish().RefCount();
            return Observable.Zip(
                Enumerable.Range(0, size)
                    .Select(skip => feed.Skip(skip))
                    .ToArray());
        }
        
        public static IObservable<IList<T>> SlidingWindow<T>(this IObservable<T> source, Func<T, DateTime> timestamp, TimeSpan timeFrame)
        {
            return source
                .SelectMany(start => source
                    .SkipWhile(x => timestamp(x) < timestamp(start))
                    .TakeWhile(x => timestamp(x) <= timestamp(start).Add(timeFrame)).ToList()
                );
        }
    }
}
