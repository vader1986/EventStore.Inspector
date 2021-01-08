using System;
using System.Text;
using EventStore.ClientAPI;
using EventStore.Inspector.Common.Extensions;
using NUnit.Framework;

namespace EventStore.Inspector.Testing.Bus
{
    public class EventQueue : IDisposable
    {
        private readonly Bus<ResolvedEvent> _bus;
        private readonly IDisposable _subscription;

        public EventQueue(Bus<ResolvedEvent> bus, IEventStoreConnection connection)
        {
            _bus = bus;
            _subscription = connection.AllStream().Subscribe(AddEvent);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void AddEvent(ResolvedEvent @event)
        {
            _bus.Add(@event);
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
