using System;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Models.Account;
using TEMPUS.WebSite.Models.Project;
using TEMPUS.WebSite.Security;

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
        /// Registers the user.
        /// </summary>
        [HttpGet]
        public ActionResult Register()
        {
            if (CurrentUser.User == null)
                return View();

            return RedirectToAction("Index", "Team");
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="model">The model represents register information of the user.</param>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (model == null)
            {
                //TODO: return error message.
                return RedirectToAction("Register", "Account");
            }
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
                return RedirectToAction("Register", "Account");
            }

            var user = _userQueryService.GetUserByEmail(model.Login);
            if (user != null)
            {
                //TODO: return error message.
                return RedirectToAction("Register", "Account");
            }

            var userId = Guid.NewGuid();
            var cmd = new CreateUser(new UserId(userId), model.Login, model.Password);
            _cmdSender.Send(cmd);

            return RedirectToAction("LogIn");
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            if (CurrentUser.User == null)
            {
                return View();
            }

            // TODO: Need to return to the project page.
            return RedirectToAction("Index", "Team");
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="model">The model represents login information of the user.</param>
        [HttpPost]
        public ActionResult LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
                return RedirectToAction("LogIn", "Account");
            }
            if (CurrentUser.User != null)
            {
                return RedirectToAction("Index", "Team");
            }

            var user = _userQueryService.GetUserByEmail(model.Login);

            if (user == null)
            {
                //TODO: return error message.
                return RedirectToAction("LogIn", "Account");
            }

            //TODO Login user using CustomMembershipProvider.
            CurrentUser.LogIn(model.Login, model.Password);

            //TODO Redirect to Project page.
            return RedirectToAction("Index", "Team");
        }

        /// <summary>
        /// Manages user profile.
        /// </summary>
        /// <param name="model">The model represents information for the user updating operation.</param>
        public ActionResult Edit(UpdateUserViewModel model)
        {
            if (model == null)
            {
                //TODO: return error message.
                return RedirectToAction("Edit", "Account");
            }
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
                return RedirectToAction("Edit", "Account");
            }

            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var userInfo = _userQueryService.GetUserByEmail(CurrentUser.User.Email);

            if (userInfo.Image != model.Image ||
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
            var userInfo = _userQueryService.GetUserByEmail(CurrentUser.User.Email);
            if (userInfo == null)
            {
                //TODO: return error message.
                return RedirectToAction("LogIn", "Account");
            }
            var model = new ProfileViewModel
            {
                //Age = userInfo.Age,
                Image = userInfo.Image,
                Login = userInfo.Email,
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
            var userInfo = _userQueryService.GetUserByEmail(CurrentUser.User.Email);
            if (userInfo == null)
            {
                //TODO: return error message.
                return RedirectToAction("LogIn", "Account");
            }
            //TODO: Log out user using CustomMembershipProvider.
            CurrentUser.LogOut();

            return RedirectToAction("LogIn");
        }
    }
}