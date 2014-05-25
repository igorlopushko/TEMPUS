using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Contexts;
using TEMPUS.WebSite.Models.Account;
using TEMPUS.WebSite.Models.Team;

namespace TEMPUS.WebSite.Controllers
{
    public class TeamController : Controller
    {
        private readonly IUserQueryService _userQueryService;
        private readonly ICommandSender _cmdSender;
        private readonly IProjectQueryService _projectQueryService;

        public TeamController(IUserQueryService userQueryService, ICommandSender cmdSender, IProjectQueryService projectQueryService)
        {
            if (userQueryService == null)
                throw new ArgumentNullException("userQueryService");
            if (cmdSender == null)
                throw new ArgumentNullException("cmdSender");
            if (projectQueryService == null)
                throw new ArgumentNullException("projectQueryService");

            _projectQueryService = projectQueryService;
            _userQueryService = userQueryService;
            _cmdSender = cmdSender;
        }

        [Authorize]
        public ActionResult Index()
        {
            var projectId = new ProjectId(UserContext.CurrentProjectId);

            List<UserInfo> userList = _userQueryService.GetUsersByProjectId(projectId).ToList();
            List<ProfileViewModel> modelUser =
                userList.Select(userInfo => new ProfileViewModel(userInfo.UserId,
                    userInfo.FirstName,
                    userInfo.LastName,
                    userInfo.Email,
                    userInfo.Phone,
                    userInfo.Image ?? "~/Content/images/user.png",
                    userInfo.DateOfBirth,
                    _userQueryService.GetProjectRoleForUser(projectId, new UserId(userInfo.UserId)).Name,
                    userInfo.Mood == null ? 0 : userInfo.Mood.Rate)).ToList();

            return View(new TeamViewModel { users = modelUser, projectId = projectId });
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetTeamMoods(Guid ProjectId)
        {
            var moods = _userQueryService.GetTeamMoods(new ProjectId(ProjectId));
            moods = moods.Where(x => (DateTime.Now - x.Date).Days <= 7);
            var users = new List<object>();
            var ids = moods.Select(x => x.UserId).Distinct();
            foreach (var id in ids)
            {
                var data = moods.Where(x => x.UserId == id).Select(x => new { date = x.Date.ToString("yyyy-MM-dd"), mood = x.Rate });
                var chosenUser = moods.Where(x => x.UserId == id).FirstOrDefault();
                string name = chosenUser.FirstName + " " + chosenUser.LastName;
                users.Add(new { name = name, data = data });
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}