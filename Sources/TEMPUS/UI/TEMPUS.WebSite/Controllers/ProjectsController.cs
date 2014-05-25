using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Contexts;
using TEMPUS.WebSite.Models.Project;

namespace TEMPUS.WebSite.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectQueryService _projectQueryService;
        private readonly ICommandSender _commandSender;
        private readonly IUserQueryService _userQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="commandSender">The command sender.</param>
        /// <param name="userQueryService">The user query service.</param>
        /// <exception cref="System.ArgumentNullException">When projectService or commandSender or userQueryService are null.</exception>
        public ProjectsController(IProjectQueryService projectService, ICommandSender commandSender,
            IUserQueryService userQueryService)
        {
            if (projectService == null)
                throw new ArgumentNullException("projectService");

            if (commandSender == null)
                throw new ArgumentNullException("commandSender");

            if (userQueryService == null)
                throw new ArgumentNullException("userQueryService");

            _projectQueryService = projectService;
            _commandSender = commandSender;
            _userQueryService = userQueryService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var projects = _projectQueryService.GetUserProjects(new UserId(UserContext.Current.UserId));
            var model = new List<ProjectDetailsViewModel>();

            foreach (var projectInfo in projects)
            {
                var project = new ProjectDetailsViewModel(projectInfo.Id.ToString(), 
                    projectInfo.Name, 
                    ProjectStatus.Green, 
                    DateTime.Now.AddDays(-100),
                    DateTime.Now.AddDays(100), 
                    new UserInfo { FirstName = "Tatyana", LastName = "Shatovska"}, 
                    "Department1", 
                    "Description");
                model.Add(project);
            }
            return View(model.ToArray());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateProjectViewModel
                {
                    ProjectMainInfo = new CreateProjectMainInfoViewModel(),
                    ProjectTeam = new CreateProjectTeamViewModel()
                };
            return View(PrepareCreateProjectModel(model));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateProjectViewModel model,
            [Bind(Prefix = "CreateProjectTeamViewModel.ProjectTeamMemberViewModel")]ProjectTeamMemberViewModel[] teamMembers)
        {
            model.ProjectTeam.TeamMembers = teamMembers ?? Enumerable.Empty<ProjectTeamMemberViewModel>();

            ICommand command;
            if (ModelState.IsValid)
            {
                var newProjectId = Guid.NewGuid();
                command = new CreateProject(
                    new ProjectId(newProjectId),
                    model.ProjectMainInfo.Name,
                    model.ProjectMainInfo.Description,
                    model.ProjectMainInfo.Orderer,
                    model.ProjectMainInfo.RecievingOrganization,
                    model.ProjectMainInfo.Mandatory,
                    model.ProjectMainInfo.StartDate,
                    model.ProjectMainInfo.EndDate,
                    model.ProjectMainInfo.DepartmentId,
                    model.ProjectMainInfo.PpsClassificationId,
                    new UserId(model.ProjectMainInfo.OwnerId),
                    new UserId(model.ProjectMainInfo.ManagerId));
                _commandSender.Send(command);

                //TODO: create commands for information from 2, 3, 4 steps.

                foreach (var teamMember in model.ProjectTeam.TeamMembers)
                {
                    //TODO: Remove when RoleId field added in the UI.
                    var roleId = Guid.Parse("7eb33e5e-1bd6-e311-be7f-94de8066e6a1");
                    command = new AssignUserToProject(new ProjectId(newProjectId), new UserId(teamMember.UserId), roleId, teamMember.FTE);
                    _commandSender.Send(command);
                }

                return RedirectToAction("Index");
            }
            return View(this.PrepareCreateProjectModel(model));
        }

        [Authorize]
        public ActionResult Details(string projectId)
        {
            var project = _projectQueryService.GetProjectById(new ProjectId(new Guid(projectId)));

            var model = new ProjectDetailsViewModel(project.Id.ToString(),
                                                    project.Name,
                                                    ProjectStatus.Green,
                                                    DateTime.Now.AddDays(-100),
                                                    DateTime.Now.AddDays(100),
                                                    new UserInfo {FirstName = "Tatyana", LastName = "Shatovska"},
                                                    "Department1",
                                                    "Description");

            return View(model);
        }

        [Authorize]
        public ActionResult Plan()
        {
            return View();
        }

        [Authorize]
        public ActionResult Select()
        {
            var projects = _projectQueryService.GetUserProjects(new UserId(UserContext.Current.UserId));
            var model = new List<ProjectDetailsViewModel>();

            foreach (var projectInfo in projects)
            {
                var project = new ProjectDetailsViewModel(projectInfo.Id.ToString(),
                    projectInfo.Name,
                    ProjectStatus.Green,
                    DateTime.Now.AddDays(-100),
                    DateTime.Now.AddDays(100),
                    new UserInfo { FirstName = "Tatyana", LastName = "Shatovska" },
                    "Department1",
                    "Description");
                model.Add(project);
            }
            return View(model.ToArray());
        }

        public ActionResult SelectProject(string projectId)
        {
            UserContext.CurrentProjectId = Guid.NewGuid();
            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<SelectListItem> GetDepartments()
        {
            var departments = _projectQueryService.GetDepartments().ToArray();
            return departments.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
        }

        private IEnumerable<SelectListItem> GetPpsClassifications()
        {
            var classifications = _projectQueryService.GetPpsClassifications().ToArray();
            return classifications.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
        }

        private IEnumerable<SelectListItem> GetUsers()
        {
            var users = _userQueryService.GetAllActiveUsers().ToArray();
            return users.Select(x => new SelectListItem { Value = x.UserId.ToString(), Text = x.Email });
        }

        private CreateProjectViewModel PrepareCreateProjectModel(CreateProjectViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.ProjectMainInfo.Departments = GetDepartments();
            model.ProjectMainInfo.PpsClassifications = GetPpsClassifications();
            model.ProjectMainInfo.Managers = GetUsers();
            model.ProjectMainInfo.Owners = GetUsers();

            model.ProjectTeam.Users = _userQueryService.GetAllActiveUsers();
            model.Roles = this.GetProjectRoles();
            return model;
        }

        private IEnumerable<SelectListItem> GetProjectRoles()
        {
            var projectRoles = _projectQueryService.GetProjectRoles();
            return projectRoles.Select(x => new SelectListItem { Value = x.Key.ToString(), Text = x.Value });
        }
    }
}