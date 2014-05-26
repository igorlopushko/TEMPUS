using System.Linq;
using System.Web;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.WebSite.Security
{
    public static class SiteSecurity
    {
        public static bool UserInRole(UserRole userRole)
        {
            return HttpContext.Current.User != null && HttpContext.Current.User.IsInRole(userRole.ToString());
        }

        public static bool UserInRole(UserInfo user, UserRole userRole)
        {
            return user != null && user.Roles.Any(role => role == userRole);
        }

        public static bool UserInRoles(params UserRole[] userRoles)
        {
            return HttpContext.Current.User != null && userRoles.Any(userRole => HttpContext.Current.User.IsInRole(userRole.ToString()));
        }

        public static bool UserInRoles(UserInfo user, params UserRole[] userRoles)
        {
            return HttpContext.Current.User != null && userRoles.Any(userRole => user.Roles.Any(role => role == userRole));
        }
    }
}