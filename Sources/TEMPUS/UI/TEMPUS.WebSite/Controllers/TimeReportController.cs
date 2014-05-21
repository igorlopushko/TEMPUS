using System;
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
                    Task = task1,
                    User = admin,
                    Hours = 4.0,
                    Date = new DateTime(2014, 04, 18)
                },
                new TimeRecordViewModel() {
                    Project = tempus,
                    Task = task1,
                    User = volkov,
                    Hours = 16,
                    Date = new DateTime(2014, 04, 18)
                },
                new TimeRecordViewModel() {
                    Project = happy,
                    Task = task3,
                    User = doe,
                    Hours = 7.5,
                    Date = new DateTime(2014, 04, 14)
                },
                new TimeRecordViewModel() {
                    Project = lucky,
                    Task = task3,
                    User = zayats,
                    Hours = 11,
                    Date = new DateTime(2014, 05, 14)
                },
                new TimeRecordViewModel() {                    
                    Project = happy,
                    Task = task3,
                    User = doe,
                    Hours = 3,
                    Date = new DateTime(2014, 04, 20)
                },
                new TimeRecordViewModel() {                    
                    Project = happy,
                    Task = task3,
                    User = admin,
                    Hours = 7,
                    Date = new DateTime(2014, 04, 19)
                },
            };

            return View(records);
        }
    }
}