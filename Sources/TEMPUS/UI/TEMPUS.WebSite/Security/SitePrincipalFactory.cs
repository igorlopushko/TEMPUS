using System.Security.Principal;
using TEMPUS.WebSite.Contexts;

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

            return new SitePrincipal(principal, UserContext.Current);
        }
    }
}