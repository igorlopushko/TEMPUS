using System;
using System.Web.Mvc;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.WebSite.Models.Project;

namespace TEMPUS.WebSite.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectQueryService _projectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <exception cref="System.ArgumentNullException">When projectService is null.</exception>
        public ProjectsController(IProjectQueryService projectService)
        {
            if(projectService == null)
                throw new ArgumentNullException("projectService");

            _projectService = projectService;
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
        public ActionResult Create()
        {
            return View();
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
    }
}