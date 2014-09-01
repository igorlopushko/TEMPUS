using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.WebSite.Controllers
{
    public class RisksController : Controller
    {
        private readonly IProjectQueryService _projectQueryService;
        private readonly ICommandSender _commandSender;
        private readonly IUserQueryService _userQueryService;

        public RisksController(IProjectQueryService projectService, ICommandSender commandSender,
            IUserQueryService userQueryService)
        {
            if (projectService == null)
            {
                throw new ArgumentNullException("projectService");
            }

            if (commandSender == null)
            {
                throw new ArgumentNullException("commandSender");
            }

            if (userQueryService == null)
            {
                throw new ArgumentNullException("userQueryService");
            }

            _projectQueryService = projectService;
            _commandSender = commandSender;
            _userQueryService = userQueryService;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
