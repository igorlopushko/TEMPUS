using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
﻿
namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserQueryService
    {
        UserInfo GetUserById(UserId id);
        UserInfo GetUserByEmail(string emails);
        IEnumerable<UserInfo> GetUsersByProjectId(ProjectId projectId);
    }
}