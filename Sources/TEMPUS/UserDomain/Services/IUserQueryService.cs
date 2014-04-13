using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Services
{
    public interface IUserQueryService
    {
        UserInfo GetUserById(UserId id);
    }
}