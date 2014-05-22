using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Models.User;
using TEMPUS.WebSite.Security;

namespace TEMPUS.WebSite.Controllers
{
    /// <summary>
    /// The class responsible for perform actions related to manage users.
    /// </summary>
    public class UserController : Controller
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _commandSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userQueryService">The user query service.</param>
        /// <param name="commandSender">The command sender.</param>
        /// <exception cref="System.ArgumentNullException">When userQueryService or commandSender are null.</exception>
        public UserController(IUserQueryService userQueryService, ICommandSender commandSender)
        {
            if (userQueryService == null)
            {
                throw new ArgumentNullException("userQueryService");
            }

            if (commandSender == null)
            {
                throw new ArgumentNullException("commandSender");
            }

            _userQueryService = userQueryService;
            _commandSender = commandSender;
        }

        [HttpGet]
        [AllowedRoles(UserRole.Administrator)]
        public ActionResult Index()
        {
            var userRoles = _userQueryService.GetUsersRoles().ToArray();
            var model = _userQueryService.GetUsers().Select(x => new UserListViewModel
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserRoles = this.GetUserRoles(userRoles, x.Roles.FirstOrDefault()),
                IsDeleted = x.IsDeleted
            });
            
            return View(model);
        }

        private IEnumerable<SelectListItem> GetUserRoles(IEnumerable<KeyValuePair<Guid, string>> userRoles, UserRole activeRole)
        {
            return userRoles.Select(x => new SelectListItem { Value = x.Key.ToString(), Text = x.Value, Selected = x.Value == activeRole.ToString() });
        }
    }
}