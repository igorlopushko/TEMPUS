using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.WebSite.Models.Project;
using TEMPUS.WebSite.Models.Team;

namespace TEMPUS.WebSite.Controllers
{
    public class TimeReportController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ProjectViewModel tempus = new ProjectViewModel()
            {
                Name = "TEMPUS"
            };

            ProjectViewModel happy = new ProjectViewModel()
            {
                Name = "Happy Project"
            };

            ProjectViewModel lucky = new ProjectViewModel()
            {
                Name = "Lucky Project"
            };

            UserViewModel admin = new UserViewModel()
            {
                FirstName = "Yaroslav",
                LastName = "Admin"
            };

            UserViewModel zayats = new UserViewModel()
            {
                FirstName = "Alexander",
                LastName = "Zayats"
            };

            UserViewModel volkov = new UserViewModel()
            {
                FirstName = "Dmitriy",
                LastName = "Volkov"
            };

            UserViewModel doe = new UserViewModel()
            {
                FirstName = "John",
                LastName = "Doe"
            };

            TaskViewModel task1 = new TaskViewModel()
            {
                Name = "Setup Online Demo",
                Status = TaskStatus.InProgress
            };

            TaskViewModel task2 = new TaskViewModel()
            {
                Name = "Add ACL System",
                Status = TaskStatus.New
            };

            TaskViewModel task3 = new TaskViewModel()
            {
                Name = "Make People Happy",
                Status = TaskStatus.InProgress
            };

            TimeRecordViewModel[] records = new TimeRecordViewModel[] {
                new TimeRecordViewModel() {
                    Project = tempus,
                    User = admin,
                    Hours = 4.0,
                    Description = "Task is done"
                },
                new TimeRecordViewModel() {
                    Project = tempus,
                    Task = task1,
                    User = volkov,
                    Hours = 16,
                    StartDate = new DateTime(2014, 04, 18),
                    EndDate = new DateTime(2014, 04, 22),
                    Description = "Create part of the task"
                },
                new TimeRecordViewModel() {
                    Project = happy,
                    Task = task3,
                    User = doe,
                    Hours = 7.5,
                    StartDate = new DateTime(2014, 04, 14),
                    EndDate = new DateTime(2014, 04, 17),
                    Description = "Have a problem for now"
                },
                new TimeRecordViewModel() {
                    Project = lucky,
                    User = zayats,
                    Hours = 11,
                    StartDate = new DateTime(2014, 05, 14),
                    EndDate = new DateTime(2014, 05, 17),
                },
                new TimeRecordViewModel() {                    
                    Project = happy,
                    Task = task3,
                    User = doe,
                    Hours = 3,
                    StartDate = new DateTime(2014, 04, 20),
                    EndDate = new DateTime(2014, 04, 21),
                },
                new TimeRecordViewModel() {                    
                    Project = happy,
                    Task = task3,
                    User = admin,
                    Hours = 7,
                    StartDate = new DateTime(2014, 04, 19),
                    EndDate = new DateTime(2014, 04, 23),
                },
            };
            
            return View(records);
        }
    }
}