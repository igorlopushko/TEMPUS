using System;
using System.Web;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.WebSite.Contexts
{
    public class UserContext
    {
        private static UserInfo User;

        /// <summary>
        /// Gets or sets a value indicating whether need to update the current user.
        /// </summary>
        public static bool IsUpdated { get; set; }

        public static UserInfo Current
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    User = null;
                    return null;
                }
                if (User == null || !IsUpdated)
                {
                    IsUpdated = true;

                    var userSvc = Container.Get<IUserQueryService>();
                    User = userSvc.GetUserByEmail(HttpContext.Current.User.Identity.Name);
                }

                return User;
            }
        }

        public static Guid CurrentProjectId
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["projectId"] != null)
                {
                    return new Guid(HttpContext.Current.Request.Cookies["projectId"].Value);
                }
                return Guid.Empty;
            }
            set
            {
                var userCookie = new HttpCookie("projectId", value.ToString());
                userCookie.Expires.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(userCookie);
            }
        }
    }
}