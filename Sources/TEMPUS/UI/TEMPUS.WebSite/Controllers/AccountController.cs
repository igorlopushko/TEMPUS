using System;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Services;
using TEMPUS.WebSite.Models.Account;

namespace TEMPUS.WebSite.Controllers
{
    /// <summary>
    /// The class represents controller for user management.
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _cmdSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userQueryService">The user query service.</param>
        /// <param name="cmdSender">The command sender.</param>
        /// <exception cref="System.ArgumentNullException">When  userQueryService or cmdSender are null.</exception>
        public AccountController(IUserQueryService userQueryService, ICommandSender cmdSender)
        {
            if (userQueryService == null)
                throw new ArgumentNullException("userQueryService");
            if (cmdSender == null)
                throw new ArgumentNullException("cmdSender");

            _userQueryService = userQueryService;
            _cmdSender = cmdSender;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="model">The model represents register information of the user.</param>
        public ActionResult Register(RegisterViewModel model)
        {
            if (model == null)
            {
                //TODO: return error message.
            }
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
            }

            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var user = _userQueryService.GetUserByLogin(model.Login);
            if (user != null)
            {
                //TODO: return error message.
            }

            var userId = Guid.NewGuid();
            var cmd = new CreateUser(new UserId(userId), model.Login, model.Password);
            _cmdSender.Send(cmd);

            return RedirectToAction("LogIn");
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="model">The model represents login information of the user.</param>
        public ActionResult LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
            }
            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var user = _userQueryService.GetUserByLogin(model.Login);
            if (user == null)
            {
                //TODO: return error message.
            }
            //TODO Login user using CustomMembershipProvider.
            //TODO Redirect to Project page.
            return RedirectToAction("Index", "Team");
        }

        /// <summary>
        /// Manages user profile.
        /// </summary>
        /// <param name="model">The model represents information for the user updating operation.</param>
        public ActionResult Manage(UpdateUserViewModel model)
        {
            if (model == null)
            {
                //TODO: return error message.
            }
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
            }

            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var userInfo = _userQueryService.GetUserByLogin(User.Identity.Name);

            if (userInfo.Age != model.Age || userInfo.Image != model.Image ||
                userInfo.Phone != model.Phone || userInfo.Password != model.Password ||
                userInfo.FirstName != model.FirstName || userInfo.LastName != model.LastName)
            {
                var command = new ChangeUserInformation(userInfo.UserId, model.Age, model.Phone, model.Image,
                    model.Password, model.FirstName, model.LastName);
                _cmdSender.Send(command);
            }

            return RedirectToAction("Profile");
        }

        /// <summary>
        /// Returns the profile of the current user.
        /// </summary>
        public new ActionResult Profile()
        {
            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var userInfo = _userQueryService.GetUserByLogin(User.Identity.Name);
            if (userInfo == null)
            {
                //TODO: return error message.
            }
            var model = new ProfileViewModel
            {
                Age = userInfo.Age,
                Image = userInfo.Image,
                Login = userInfo.Login,
                Phone = userInfo.Phone,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };

            return View(model);
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public ActionResult LogOut()
        {
            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var userInfo = _userQueryService.GetUserByLogin(User.Identity.Name);
            if (userInfo == null)
            {
                //TODO: return error message.
            }
            //TODO: Log off user using CustomMembershipProvider.
            return RedirectToAction("LogIn");
        }
    }
}