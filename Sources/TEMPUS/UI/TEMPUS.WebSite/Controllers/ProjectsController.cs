using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.UserDomain.Services.ServiceLayer;
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
            ProjectListViewModel[] projects = new ProjectListViewModel[] {
                new ProjectListViewModel() {
                    Name = "Project1",
                    Status = ProjectStatus.Green,
                    StartDate = DateTime.Now.AddDays(-100),
                    EndDate = DateTime.Now,
                    Manager = "John Walk",
                    Department = "Department1",
                    Description = "Description1"
                },
                new ProjectListViewModel() {
                    Name = "Project2",
                    Status = ProjectStatus.Red,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Manager = "Peter Jackson",
                    Department = "Department2",
                    Description = "Description2"
                },
                new ProjectListViewModel() {
                    Name = "Project3",
                    Status = ProjectStatus.Red,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Manager = "Jim Cleverly",
                    Department = "Department3",
                    Description = "Description3"
                },
                 new ProjectListViewModel() {
                    Name = "Project4",
                    Status = ProjectStatus.Yellow,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Manager = "Jim Cleverly",
                    Department = "Department4",
                    Description = "Description4"
                }
            };
            return View(projects);
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
            return View(this.PrepareCreateProjectModel(model));
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
                    command = new AssignUserToProject(new ProjectId(newProjectId), new UserId(teamMember.UserId), teamMember.RoleId, teamMember.FTE);
                    _commandSender.Send(command);
                }

                return RedirectToAction("Index");
            }
            return View(this.PrepareCreateProjectModel(model));
        }

        [Authorize]
        public ActionResult Details()
        {
            return View();
        }

        [Authorize]
        public ActionResult Plan()
        {
            return View();
        }

        [Authorize]
        public ActionResult Select()
        {
            ProjectListViewModel[] projects = new ProjectListViewModel[] {
                new ProjectListViewModel() {
                    Name = "Project1",
                    Status = ProjectStatus.Green,
                    StartDate = DateTime.Now.AddDays(-100),
                    EndDate = DateTime.Now,
                    Manager = "John Walk",
                    Department = "Department1",
                    Description = "Description1"
                },
                new ProjectListViewModel() {
                    Name = "Project2",
                    Status = ProjectStatus.Red,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Manager = "Peter Jackson",
                    Department = "Department2",
                    Description = "Description2"
                },
                new ProjectListViewModel() {
                    Name = "Project3",
                    Status = ProjectStatus.Red,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Manager = "Jim Cleverly",
                    Department = "Department3",
                    Description = "Description3"
                },
                 new ProjectListViewModel() {
                    Name = "Project4",
                    Status = ProjectStatus.Yellow,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Manager = "Jim Cleverly",
                    Department = "Department4",
                    Description = "Description4"
                }
            };
            return View(projects);
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

            model.ProjectMainInfo.Departments = this.GetDepartments();
            model.ProjectMainInfo.PpsClassifications = this.GetPpsClassifications();
            model.ProjectMainInfo.Managers = this.GetUsers();
            model.ProjectMainInfo.Owners = this.GetUsers();

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