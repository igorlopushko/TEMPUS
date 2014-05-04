using System.Security.Principal;
using TEMPUS.Infrastructure.Unity;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.WebSite.Security
{
    /// <summary>
    /// Represents a class for creation custom site principals
    /// </summary>
    public static class SitePrincipalFactory
    {
        /// <summary>
        /// Creates Site principal
        /// </summary>
        /// <param name="principal"><see cref="IPrincipal"/> instance</param>
        /// <returns>Returns Site principal</returns>
        public static IPrincipal CreatePrincipal(IPrincipal principal)
        {
            if (principal == null)
            {
                return null;
            }

            var userName = principal.Identity.Name;
            var userQueryService = Container.Get<IUserQueryService>();
            var user = userQueryService.GetUserByEmail(userName);
            return new SitePrincipal(principal, user);
        }
    }
}