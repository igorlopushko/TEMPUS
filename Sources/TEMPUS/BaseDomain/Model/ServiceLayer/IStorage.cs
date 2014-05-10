using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TEMPUS.DB.Models;

namespace TEMPUS.BaseDomain.Model.ServiceLayer
{
    public interface IStorage<T, TId>
        where T : Entity
    {
        T Get(TId id);

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void Delete(T entity);

        void Update(T aggregate);
    }
}
