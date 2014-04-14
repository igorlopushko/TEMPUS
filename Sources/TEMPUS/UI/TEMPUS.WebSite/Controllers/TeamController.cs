using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Services;
using TEMPUS.WebSite.Models.Team;

namespace TEMPUS.WebSite.Controllers
{
    public class TeamController : Controller
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _cmdSender;

        public TeamController(IUserQueryService userQueryService, ICommandSender cmdSender)
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
            //TODO: get the real team list
            //ProjectId teamId = null;
            //var Team = _userQueryService.GetUsersByProjectId(teamId);



            //this team is stub
            UserViewModel[] Team = {
                                       new UserViewModel
                                       {
                                           FirstName = "Tetyana",
                                           LastName = "Shatovska",
                                           Image = "~/Content/images/user.png",
                                           Role = "Project Manager"
                                       },
                                       new UserViewModel
                                       {
                                           FirstName = "Igor",
                                           LastName = "Lopushko",
                                           Image = "~/Content/images/user.png",
                                           Role = "Tech lead"
                                       },
                                       new UserViewModel
                                       {
                                           FirstName = "Yaroslav",
                                           LastName = "Admin",
                                           Image = "~/Content/images/user.png",
                                           Role = "Developer"
                                       },
                                       new UserViewModel
                                       {
                                           FirstName = "Anatolii",
                                           LastName = "Ovchinnikov",
                                           Image = "~/Content/images/user.png",
                                           Role = "Developer"
                                       },
                                       new UserViewModel
                                       {
                                           FirstName = "Alexandra",
                                           LastName = "Yugan",
                                           Image = "~/Content/images/user.png",
                                           Role = "Developer"
                                       },
                                       new UserViewModel
                                       {
                                           FirstName = "Alexander",
                                           LastName = "Zayac",
                                           Image = "~/Content/images/user.png",
                                           Role = "Developer"
                                       },
                                       new UserViewModel
                                       {
                                           FirstName = "Volkov",
                                           LastName = "Dmitriy",
                                           Image = "~/Content/images/user.png",
                                           Role = "Developer"
                                       },
                                   };
            return View(Team.ToList());
        }

    }
}
