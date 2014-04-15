using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;

namespace TEMPUS.BaseDomain.Model.ServiceLayer
{
    public interface IReadStorage<T> where T : Dto
    {
        T Get(UserId id);

        IQueryable<T> All { get; }

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);
    }
}
