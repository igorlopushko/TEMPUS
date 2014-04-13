using System;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserQueryService : IUserQueryService
    {
        public UserInfo GetUserById(UserId id)
        {
            return new UserInfo();
        }

        public UserInfo GetUserByLogin(string login)
        {
            throw new NotImplementedException();
        }
    }
}