using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEMPUS.UserDomain.Model;

namespace TEMPUS.UserDomain.Services
{
    public interface IUserQueryService
    {
        UserInfo GetUserById(string id);
    }
}