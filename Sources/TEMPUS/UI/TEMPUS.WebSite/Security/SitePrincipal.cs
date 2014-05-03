using System;
using System.Linq;
using System.Security.Principal;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.WebSite.Security
{
    /// <summary>
    /// SitePrincipal represents an RTD implementation of IPrincipal interface
    /// </summary>
    public class SitePrincipal : IPrincipal
    {
        private readonly IPrincipal _principal;

        private readonly UserInfo _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitePrincipal" /> class.
        /// </summary>
        /// <param name="principal"><see cref="IPrincipal"/> instance</param>
        /// <param name="user"><see cref="UserInfo"/> instance</param>
        public SitePrincipal(IPrincipal principal, UserInfo user)
        {
            _user = user;
            _principal = principal;
        }

        /// <summary>
        /// Gets Identity
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return _principal.Identity;
            }
        }

        /// <summary>
        /// Checks if current user has a required role
        /// </summary>
        /// <param name="role">Role</param>
        /// <returns>Returns true if user has role, otherwise false</returns>
        public bool IsInRole(string role)
        {
            if (_principal != null && _principal.IsInRole(role))
            {
                return true;
            }

            return _user != null && _user.Roles.Select(r => r.ToString()).Contains(role, StringComparer.OrdinalIgnoreCase);
        }
    }
}