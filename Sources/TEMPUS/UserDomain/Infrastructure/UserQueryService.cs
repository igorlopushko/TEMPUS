using System;
using System.Collections.Generic;
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

        public UserInfo GetUserByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> GetUsersByProjectId(ProjectId projectId)
        {
            throw new NotImplementedException();
        }
    }
}