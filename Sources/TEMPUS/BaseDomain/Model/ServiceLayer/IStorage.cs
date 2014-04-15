using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.BaseDomain.Model.ServiceLayer
{
    public interface IStorage<T, TId>
        where T : AggregateRoot<TId>
        where TId : GuidIdentity
    {
        T Get(UserId id);

        IQueryable<T> All { get; }

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);

        void Add(T aggregate);

        void Delete(T aggregate);

        void Update(T aggregate);
    }
}
