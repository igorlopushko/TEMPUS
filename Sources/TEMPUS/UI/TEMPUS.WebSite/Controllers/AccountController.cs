using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model;
using TEMPUS.UserDomain.Services;
using TEMPUS.WebSite.Models.Account;

namespace TEMPUS.WebSite.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _cmdSender;
        
        public AccountController(
            IUserQueryService userQueryService,
            ICommandSender cmdSender)
        {
            _userQueryService = userQueryService;
            _cmdSender = cmdSender;
        }

        public ActionResult Register(RegisterPageViewModel model)
        {
            var cmd = new CreateUser(new UserId(new Guid()));
            _cmdSender.Send(cmd);

            return View();
        }

        public ActionResult Profile()
        {
            UserInfo info = _userQueryService.GetUserById("123");

            //TODO: convert to ViewModel
            var model = new ProfilePageViewModel();

            return View(model);
        }
    }
}