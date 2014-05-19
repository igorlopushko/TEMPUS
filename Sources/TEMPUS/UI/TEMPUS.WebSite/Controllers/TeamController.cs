using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Services;
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
            //Next two lines are just stub, don't look at them!
            Guid id = _projectQueryService
                .GetUserProjects(_userQueryService.GetUserByEmail("shatovska@gmail.com").UserId).FirstOrDefault().Id;
            ProjectId projectId = new ProjectId(id);

            var Team = _userQueryService.GetUsersByProjectId(projectId).Select(x => new ProfileViewModel
            {
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                FirstName = x.FirstName,
                Image = x.Image == null ? "~/Content/images/user.png" : x.Image,
                LastName = x.LastName,
                UserId = x.UserId,
                Role = _userQueryService.GetProjectRoleForUser(projectId, x.UserId).Name
            }).OrderBy(x => x.Role).AsEnumerable();
            return View(Team.ToList());
        }
    }
}