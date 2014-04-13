using System.Collections.Generic;

namespace TEMPUS.BaseDomain.Messages
{
    public interface IEventStore
    {
        void SaveAggregateEvents<T>(T aggregateId, string aggregateType, IEnumerable<IEvent<T>> events)
                                    where T : IIdentity;

        IEnumerable<IEvent<T>> GetEventsForAggregate<T>(T aggregateId)
                                    where T : IIdentity; 
    }
}