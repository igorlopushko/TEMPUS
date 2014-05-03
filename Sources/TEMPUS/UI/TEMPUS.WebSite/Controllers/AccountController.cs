using System;
using System.Web.Mvc;
using System.Web.Routing;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Contexts;
using TEMPUS.WebSite.Interfaces;
using TEMPUS.WebSite.Models.Account;
using TEMPUS.WebSite.Security;
using TEMPUS.WebSite.Services;

namespace TEMPUS.WebSite.Controllers
{
    /// <summary>
    /// The class represents controller for user management.
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _cmdSender;
        private IFormsAuthenticationService _formsService;
        private IMembershipService _membershipService;

        protected override void Initialize(RequestContext requestContext)
        {
            if (_formsService == null)
            {
                _formsService = new FormsAuthenticationService();
            }
            if (_membershipService == null)
            {
                _membershipService = new AccountMembershipService();
            }

            base.Initialize(requestContext);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userQueryService">The user query service.</param>
        /// <param name="cmdSender">The command sender.</param>
        /// <exception cref="System.ArgumentNullException">When  userQueryService or cmdSender are null.</exception>
        public AccountController(
            IUserQueryService userQueryService, 
            ICommandSender cmdSender)
        {
            if (userQueryService == null)
            {
                throw new ArgumentNullException("userQueryService");
            }
            if (cmdSender == null)
            {
                throw new ArgumentNullException("cmdSender");
            }

            _userQueryService = userQueryService;
            _cmdSender = cmdSender;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult Register()
        {
            if (UserContext.Current == null)
            {
                return View();
            }

            return RedirectToAction("Index", "Team");
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="model">The model represents register information of the user.</param>
        [HttpPost]
        [Authorize]
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
            if (UserContext.Current == null)
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
            if (ModelState.IsValid)
            {
                if (_membershipService.ValidateUser(model.Login, model.Password))
                {
                    _formsService.SignIn(model.Login, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public ActionResult LogOut()
        {
            _formsService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult UserMenu()
        {
            var model = new UserMenuViewModel(UserContext.Current.FirstName, UserContext.Current.LastName);
            return DisplayFor(model);
        }

        /// <summary>
        /// Manages user profile.
        /// </summary>
        /// <param name="model">The model represents information for the user updating operation.</param>
        [Authorize]
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
            var userInfo = _userQueryService.GetUserByEmail(UserContext.Current.Email);

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
        [Authorize]
        public new ActionResult Profile()
        {
            // TODO Change GetUserByLogin for CurrentUser.User when implemented.
            var userInfo = _userQueryService.GetUserByEmail(UserContext.Current.Email);
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
    }
}