using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.WebSite.Models.Project;
using TEMPUS.WebSite.Models.Task;

namespace TEMPUS.WebSite.Controllers
{
    public class TasksController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            TaskListViewModel[] tasks = new TaskListViewModel[] {
                new TaskListViewModel() {
                    Name = "Task1",
                    Status = TaskStatus.Done,
                    StartDate = DateTime.Now.AddDays(-100),
                    EndDate = DateTime.Now,
                    Person = "John Walk"
                },
                new TaskListViewModel() {
                    Name = "Task2",
                    Status = TaskStatus.InProgress,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(-50),
                    Person = "Peter Jackson"
                },
                new TaskListViewModel() {
                    Name = "Task3",
                    Status = TaskStatus.New,
                    StartDate = DateTime.Now.AddDays(-50),
                    EndDate = DateTime.Now.AddDays(-200),
                    Person = "Jim Cleverly",
                },
                 new TaskListViewModel() {
                    Name = "Task4",
                    Status = TaskStatus.InProgress,
                    StartDate = DateTime.Now.AddDays(-200),
                    EndDate = DateTime.Now.AddDays(-50),
                    Person = "Roman Abramovich",
                }
            };
            return View(tasks);
        }

        [Authorize]
        public ActionResult Details()
        {
            return View();
        }
    }
}