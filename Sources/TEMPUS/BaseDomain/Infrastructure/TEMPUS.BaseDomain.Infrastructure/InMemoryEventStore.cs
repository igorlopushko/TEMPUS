using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.BaseDomain.Infrastructure
{
    public class InMemoryEventStore : IEventStore
    {

        public void SaveAggregateEvents<T>(T aggregateId, string aggregateType, System.Collections.Generic.IEnumerable<IEvent<T>> events) where T : IIdentity
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<IEvent<T>> GetEventsForAggregate<T>(T aggregateId) where T : IIdentity
        {
            throw new System.NotImplementedException();
        }
    }
}