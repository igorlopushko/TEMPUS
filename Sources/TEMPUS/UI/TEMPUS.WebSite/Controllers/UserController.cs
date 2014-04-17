using System;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Models.Team;

namespace TEMPUS.WebSite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _cmdSender;

        public UserController(IUserQueryService userQueryService, ICommandSender cmdSender)
        {
            if (userQueryService == null)
                throw new ArgumentNullException("userQueryService");
            if (cmdSender == null)
                throw new ArgumentNullException("cmdSender");

            _userQueryService = userQueryService;
            _cmdSender = cmdSender;
        }

        public ActionResult Index()
        {
            //TODO: get the real user

            //this user is stub
            UserViewModel user = new UserViewModel 
            {
                Age = 26,
                Email = "email@mail.com",
                FirstName = "Igor",
                LastName = "Lopushko",
                Phone = "+380970000000",
                Role = "Tech Lead",
                Image = "~/Content/images/user.png",
                Mood = new Random().Next(1,6)
            };
            return View(user);
        }

    }
}
