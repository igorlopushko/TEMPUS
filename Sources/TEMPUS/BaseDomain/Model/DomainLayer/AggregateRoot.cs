using System.Collections.Generic;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.BaseDomain.Model.DomainLayer
{
    public abstract class AggregateRoot<T> : IAggregateRoot<T>
        where T : IIdentity
    {
        private readonly List<IEvent<T>> _changes = new List<IEvent<T>>();

        public abstract T Id { get; }

        public IEnumerable<IEvent<T>> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadFromHistory(IEnumerable<IEvent<T>> history)
        {
            foreach (var e in history)
            {
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(IEvent<T> @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IEvent<T> @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) _changes.Add(@event);
        }
    }
}