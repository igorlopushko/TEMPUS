using System;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Model.DomainLayer;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.BaseDomain.Infrastructure
{
    public abstract class Repository<TRoot, TId> : IRepository<TRoot, TId>
        where TId : IIdentity
        where TRoot : class, IAggregateRoot<TId>
    {
        protected readonly IEventStore _eventStore;

        public Repository(IEventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException("eventStore");
            }

            _eventStore = eventStore;
        }

        public abstract TRoot Get(TId id);

        public virtual void Save(TRoot root)
        {
            var events = root.GetUncommittedChanges();
            _eventStore.SaveAggregateEvents(root.Id, root.GetType().Name, events);
            root.MarkChangesAsCommitted();
        }
    }
}