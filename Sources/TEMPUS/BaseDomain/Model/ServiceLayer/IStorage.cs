using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB.Models;

namespace TEMPUS.BaseDomain.Model.ServiceLayer
{

    public interface IStorage<T>
        where T : Entity
    {
        T Get(UserId id);
        
        IEnumerable<T> Get(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void Delete(T entity);

        void Update(T aggregate);
    }
}
