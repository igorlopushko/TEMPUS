using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
﻿using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model;

namespace TEMPUS.UserDomain.Services
{
    public interface IUserQueryService
    {
        UserInfo GetUserById(UserId id);
        UserInfo GetUserByLogin(string login);
        List<UserInfo> GetUsersByProjectId(ProjectId projectId);
    }
}