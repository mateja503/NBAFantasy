
using ApplicationDefaults.Exceptions;

namespace NBA.Service.Observer
{
    public class EventManager
    {
        public Dictionary<string, HashSet<ISubscriber>> listeners = [];
        private readonly HashSet<string> eventTypes = [EventType.Auction];
        
        private void checkIfEventTypeIsSupported(string eventType)
        {
            if (!eventTypes.Contains(eventType))
                throw new NBAException($"Event type {eventType} is not supported.", ErrorCodes.TypeIsNotSupported);
        }

        public void subscribe(string eventType, ISubscriber listener)
        {
            checkIfEventTypeIsSupported(eventType);

            if (!listeners.ContainsKey(eventType))
                listeners.Add(eventType, new HashSet<ISubscriber>());

            listeners[eventType].Add(listener);
        }

        public void unsubscribe(string eventType, ISubscriber listener) 
        {
            checkIfEventTypeIsSupported(eventType); 

            if (listeners.ContainsKey(eventType))
                listeners[eventType].Remove(listener);
        }

        public void notify(string eventType, object message)
        {
            checkIfEventTypeIsSupported(eventType);

            if (listeners.ContainsKey(eventType))
            {
                foreach (var listener in listeners[eventType])
                     listener.HandleMessage(message);
            }
        }
    }
}
