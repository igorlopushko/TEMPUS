using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.WebSite.Contexts
{
    public class UserContext
    {
        public static UserInfo Current
        {
            get
            {
                var userSvc = Container.Get<IUserQueryService>();
                var user = userSvc.GetUserByEmail(HttpContext.Current.User.Identity.Name);
                return user;
            }
        }
    }
}