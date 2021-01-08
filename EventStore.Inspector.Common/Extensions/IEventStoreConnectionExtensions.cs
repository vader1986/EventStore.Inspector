using System;
using System.Reactive.Linq;
using EventStore.ClientAPI;

namespace EventStore.Inspector.Common.Extensions
{
    public static class IEventStoreConnectionExtensions
    {
        private static void OnEvent<T>(this IObserver<T> observer, EventStoreSubscription _, T @event)
        {
            observer.OnNext(@event);
        }

        private static void OnDrop<T>(this IObserver<T> observer, EventStoreSubscription _, SubscriptionDropReason reason, Exception e)
        {
            if (reason == SubscriptionDropReason.UserInitiated)
            {
                observer.OnCompleted();
            }
            else
            {
                observer.OnError(e);
            }
        }

        public static IObservable<ResolvedEvent> AllStream(this IEventStoreConnection connection)
        {
            return Observable.Create<ResolvedEvent>(async observer => {
                await connection.SubscribeToAllAsync(true, observer.OnEvent, observer.OnDrop);
            }).Catch((Exception e) => connection.AllStream());
        }
    }
}
