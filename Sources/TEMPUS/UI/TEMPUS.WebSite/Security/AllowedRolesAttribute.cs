using System;
using System.Linq;
using System.Web.Mvc;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.WebSite.Security
{
    /// <summary>
    /// Represents an attribute that is used to restrict access by callers to an action method.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AllowedRolesAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllowedRolesAttribute" /> class.
        /// </summary>
        /// <param name="userRoles">Required user roles</param>
        public AllowedRolesAttribute(params UserRole[] userRoles)
        {
            Roles = string.Join(",", userRoles.Select(r => r.ToString()));
        }
    }
}