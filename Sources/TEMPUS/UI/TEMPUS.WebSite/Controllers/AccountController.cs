﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
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
                _membershipService = new AccountMembershipService(null, _cmdSender);
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
        [AllowedRoles(UserRole.Administrator)]
        public ActionResult Register()
        {
            ViewBag.UserRoles = this.GetUserRoles();
            return View();
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="model">The model represents register information of the user.</param>
        [HttpPost]
        [Authorize]
        [AllowedRoles(UserRole.Administrator)]
        public ActionResult Register(RegisterViewModel model)
        {
            if (model == null)
            {
                //TODO: return error message.
                return RedirectToAction("Register", "Account");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.UserRoles = this.GetUserRoles();
                return View(model);
            }

            var user = _userQueryService.GetUserByEmail(model.Email);
            if (user != null)
            {
                //TODO: return error message.
                return RedirectToAction("Register", "Account");
            }

            var result = _membershipService.CreateUser(model.FirstName, model.Password, model.Email, model.LastName, model.RoleId, model.DateOfBirth);

            if (result == MembershipCreateStatus.Success)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                SetErrorMessage(result);
                ViewBag.UserRoles = this.GetUserRoles();
                return View(model);
            }
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
                if (_membershipService.ValidateUser(model.Email, model.Password))
                {
                    _formsService.SignIn(model.Email, model.RememberMe);

                    var user = _userQueryService.GetUserByEmail(model.Email);
                    if(SiteSecurity.UserInRole(user, UserRole.Administrator))
                    {
                        UserContext.IsUpdated = false;
                        return RedirectToAction("Index", "User");
                    }
                    return RedirectToAction("Select", "Projects");
                }
                ModelState.AddModelError("Error", "The user name or password provided is incorrect.");
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
            UserContext.IsUpdated = false;
            UserContext.CurrentProjectId = Guid.Empty;
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult UserMenu()
        {
            var model = new UserMenuViewModel(UserContext.Current.FirstName, UserContext.Current.LastName);
            return DisplayFor(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditProfile()
        {
            return View();
        }

        /// <summary>
        /// Manages user profile.
        /// </summary>
        /// <param name="model">The model represents information for the user updating operation.</param>
        [Authorize]
        [HttpPost]
        public ActionResult Profile(ProfileViewModel model)
        {
            if (model == null)
            {
                //TODO: return error message.
                return View();
            }
            if (!ModelState.IsValid)
            {
                //TODO: return error message.
                return View("EditProfile", model);
            }

            var userInfo = _userQueryService.GetUser(new UserId(UserContext.Current.UserId));
            if (userInfo == null)
            {
                //TODO: return error message.
                return RedirectToAction("Profile", new { id = UserContext.Current.UserId });
            }

            if (userInfo.FirstName != model.FirstName || userInfo.LastName != model.LastName || userInfo.Phone != model.Phone || userInfo.DateOfBirth != model.DateOfBirth)
            {
                var command = new ChangeUserInformation(new UserId(userInfo.UserId), model.Phone, userInfo.Image, model.FirstName,
                    model.LastName, model.DateOfBirth);
                _cmdSender.Send(command);

                UserContext.IsUpdated = false;
            }

            return RedirectToAction("Profile", new { id = userInfo.UserId });
        }

        /// <summary>
        /// Returns the profile of the current user.
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult Profile(string id)
        {
            var userInfo = _userQueryService.GetUser(new UserId(new Guid(id)));
            if (userInfo == null)
            {
                //TODO: Set the error message.
                return View();
            }

            var model = 
                new ProfileViewModel(userInfo.UserId, 
                    userInfo.FirstName, 
                    userInfo.LastName, 
                    userInfo.Email, 
                    userInfo.Phone, 
                    userInfo.Image ?? "~/Content/images/user.png", 
                    userInfo.DateOfBirth, 
                    userInfo.Roles == null ? null : userInfo.Roles.FirstOrDefault().ToString(),
                    userInfo.Mood == null ? 0 : userInfo.Mood.Rate);

            return View(model);
        }

        /// <summary>
        /// Sets the mood.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">model is null.</exception>
        /// <exception cref="System.ArgumentException">
        /// User can change rate of the mood only for himself.
        /// or
        /// Rate of the mood should be more than 0 and less or equal 5.
        /// </exception>
        [HttpPost]
        [Authorize]
        public JsonResult SetMood(string UserId, int Rate)
        {
            UserMoodViewModel model = new UserMoodViewModel { Rate = Rate, UserId = new Guid(UserId) };
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.UserId != UserContext.Current.UserId)
                throw new ArgumentException("User can change rate of the mood only for himself.");

            if (model.Rate <= 0 || model.Rate > 4)
                throw new ArgumentException("Rate of the mood should be more than 0 and less or equal 4.");

            var userMood = _userQueryService.GetUserMood(new UserId(model.UserId));
            if (userMood != null)
                throw new ArgumentException("User can set his mood only one time per day.");

            var command = new SetUserMood(new UserId(model.UserId), model.Rate);

            _cmdSender.Send(command);

            return Json(new { success = true, rate = model.Rate });
        }

        private void SetErrorMessage(MembershipCreateStatus status)
        {
            //TODO: Handle all the status.
            switch (status)
            {
                case MembershipCreateStatus.InvalidEmail:
                    {
                        ModelState.AddModelError("Error", "Invalid Email.");
                        break;
                    }
                case MembershipCreateStatus.DuplicateEmail:
                    {
                        ModelState.AddModelError("Error", "Such Email already exist in the system.");
                        break;
                    }
            }
        }

        private IEnumerable<SelectListItem> GetUserRoles()
        {
            var userRoles = _userQueryService.GetUsersRoles();
            return new List<SelectListItem>(userRoles.Select(x => new SelectListItem { Value = x.Key.ToString(), Text = x.Value }).ToList());
        }
    }
}