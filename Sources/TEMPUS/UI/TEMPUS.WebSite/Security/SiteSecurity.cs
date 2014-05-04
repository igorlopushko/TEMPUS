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

        public static bool UserInRoles(params UserRole[] userRoles)
        {
            return HttpContext.Current.User != null && userRoles.Any(userRole => HttpContext.Current.User.IsInRole(userRole.ToString()));
        }
    }
}