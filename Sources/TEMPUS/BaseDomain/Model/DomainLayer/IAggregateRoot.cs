using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.BaseDomain.Model.DomainLayer
{
    public interface IAggregateRoot<T>
        where T : IIdentity
    {
        T Id { get; }

        IEnumerable<IEvent<T>> GetUncommittedChanges();
        void MarkChangesAsCommitted();
        void LoadFromHistory(IEnumerable<IEvent<T>> history);
    }
}