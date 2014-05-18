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
        private readonly IProjectQueryService _projectService;
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

            _projectService = projectService;
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
                    ProjectMainInfo = new CreateProjectMainInfoViewModel()
                };
            return View(this.PrepareCreateProjectModel(model));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newProjectId = Guid.NewGuid();
                var command = new CreateProject(new ProjectId(newProjectId), model.ProjectMainInfo.Name,
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
            var departments = _projectService.GetDepartments().ToArray();
            return departments.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
        }

        private IEnumerable<SelectListItem> GetPpsClassifications()
        {
            var classifications = _projectService.GetPpsClassifications().ToArray();
            return classifications.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
        }

        private IEnumerable<SelectListItem> GetUsers()
        {
            var users = _userQueryService.GetUsers().ToArray();
            return users.Select(x => new SelectListItem { Value = x.UserId.Id.ToString(), Text = x.Email }).ToList();
        }

        private CreateProjectViewModel PrepareCreateProjectModel(CreateProjectViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.ProjectMainInfo.Priorities = new List<SelectListItem>
                {
                    new SelectListItem { Text = "High", Value = "1" },
                    new SelectListItem { Text = "Medium", Value = "2" },
                    new SelectListItem { Text = "Low", Value = "3" }
                };
            model.ProjectMainInfo.Departments = this.GetDepartments();
            model.ProjectMainInfo.PpsClassifications = this.GetPpsClassifications();
            model.ProjectMainInfo.Managers = this.GetUsers();
            model.ProjectMainInfo.Owners = this.GetUsers();
            model.ProjectMainInfo.Qualities = new List<SelectListItem>
                {
                    new SelectListItem { Text = "0.1", Value = "0.1" },
                    new SelectListItem { Text = "0.3", Value = "0.3" },
                    new SelectListItem { Text = "0.6", Value = "0.6" }
                };

            return model;
        }
    }
}