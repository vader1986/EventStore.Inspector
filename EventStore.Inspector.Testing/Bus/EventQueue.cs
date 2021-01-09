using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using EventStore.ClientAPI;
using NUnit.Framework;

namespace EventStore.Inspector.Testing.Bus
{
    public class EventQueue : IDisposable
    {
        private readonly Bus<ResolvedEvent> _bus;
        private readonly IEnumerable<EventStoreSubscription> _subscriptions;

        public EventQueue(Bus<ResolvedEvent> bus, IEventStoreConnection connection, IEnumerable<string> streams)
        {
            _bus = bus;
            _subscriptions = streams
                .Select(stream => connection.SubscribeToStreamAsync(stream, true, (_, e) => _bus.Add(e)))
                .Select(task => task.GetAwaiter().GetResult()).ToList();
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        public void WaitForType(string eventType)
        {
            _bus.WaitForNext(e => e.Event.EventType == eventType);
        }

        public void WaitForNext(Predicate<ResolvedEvent> predicate)
        {
            _bus.WaitForNext(predicate);
        }

        public void WaitForNext(JsonPredicate predicate)
        {
            _bus.WaitForNext(@event =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(@event.Event.Data);

                    predicate.Assert(json);
                }
                catch (AssertionException)
                {
                    return false;
                }

                return true;
            });
        }
    }
}
