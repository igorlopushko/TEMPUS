using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserQueryService : IUserQueryService
    {
        public UserInfo GetUserById(UserId id)
        {
            throw new NotImplementedException();
        }
    }
}