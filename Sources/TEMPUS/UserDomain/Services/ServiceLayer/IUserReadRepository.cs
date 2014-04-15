using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserReadRepository
    {
        UserInfo Get(UserId id);

        IQueryable<UserInfo> All { get; }

        IEnumerable<UserInfo> Get(Expression<Func<UserInfo, bool>> expression);
    }
}