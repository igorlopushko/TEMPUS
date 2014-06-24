using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.ProjectDomain.Services.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Contexts;
using TEMPUS.WebSite.Models.TimeRecord;

namespace TEMPUS.WebSite.Controllers
{
    /// <summary>
    /// The class represents functionality related to time records.
    /// </summary>
    [Authorize]
    public class TimeReportController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly ITimeRecordQueryService _timeRecordQueryService;
        private readonly IProjectQueryService _projectQueryService;
        private readonly IUserQueryService _userQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeReportController" /> class.
        /// </summary>
        /// <param name="commandSender">The sender.</param>
        /// <param name="timeRecordQueryService">The query service.</param>
        /// <param name="projectQueryService">The project query service.</param>
        /// <param name="userQueryService">The user query service.</param>
        /// <exception cref="System.ArgumentNullException">When commandSender or timeRecordQueryService or 
        /// projectQueryService or userQueryService are null.</exception>
        public TimeReportController(ICommandSender commandSender, ITimeRecordQueryService timeRecordQueryService,
            IProjectQueryService projectQueryService, IUserQueryService userQueryService)
        {
            if (commandSender == null)
            {
                throw new ArgumentNullException("commandSender");
            }

            if (timeRecordQueryService == null)
            {
                throw new ArgumentNullException("timeRecordQueryService");
            }

            if (projectQueryService == null)
            {
                throw new ArgumentNullException("projectQueryService");
            }

            if (userQueryService == null)
            {
                throw new ArgumentNullException("userQueryService");
            }

            _commandSender = commandSender;
            _timeRecordQueryService = timeRecordQueryService;
            _projectQueryService = projectQueryService;
            _userQueryService = userQueryService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new TimeRecordsListViewModel();

            return View(this.PrepareTimeRecords(model));
        }

        [HttpPost]
        public ActionResult Index(TimeRecordsListViewModel model)
        {
            var userId = new UserId(model.SelectedPerson);
            var timeRecords = _timeRecordQueryService.GetUserTimeRecords(userId)
                .Where(x => x.StartDate >= model.SelectedStartDate
                    && x.EndDate <= model.SelectedEndDate
                    && x.Project.Id == model.SelectedProject);

            if (model.SelectedStatus != TimeRecordStatus.All)
                timeRecords = timeRecords.Where(x => x.Status == (ProjectDomain.Model.DomainLayer.TimeRecordStatus)model.SelectedStatus);

            return View(this.PrepareTimeRecords(model, timeRecords.ToList(), model.SelectedProject, model.SelectedPerson));
        }

        [HttpPost]
        public ActionResult Create(TimeRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Add validation.
                var userId = new UserId(UserContext.Current.UserId);
                var timeRecordId = Guid.NewGuid();
                var command = new CreateTimeRecord(new TimeRecordId(timeRecordId), userId, new ProjectId(model.Project.ProjectId),
                    model.Description, model.Effort, model.StartDate,
                    model.EndDate);
                _commandSender.Send(command);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var timeRecordId = new TimeRecordId(id);
            var command = new DeleteTimeReport(timeRecordId);
            _commandSender.Send(command);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Notify([Bind(Prefix = "NotifiedTimeRecordViewModel")]IEnumerable<NotifiedTimeRecordViewModel> models)
        {
            if (models == null)
                return RedirectToAction("Index");

            models = models.Where(x => x.IsNotified);

            foreach (var notifiedTimeRecord in models)
            {
                var command = new NotifyTimeRecord(new TimeRecordId(notifiedTimeRecord.TimeRecordId));
                _commandSender.Send(command);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Accept(Guid id)
        {
            var timeRecordId = new TimeRecordId(id);
            var command = new AcceptTimeRecord(timeRecordId);
            _commandSender.Send(command);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Decline(Guid id)
        {
            var timeRecordId = new TimeRecordId(id);
            var command = new DeclineTimeRecord(timeRecordId);
            _commandSender.Send(command);

            return RedirectToAction("Index");
        }

        private TimeRecordsListViewModel PrepareTimeRecords(TimeRecordsListViewModel model)
        {
            return this.PrepareTimeRecords(model, Enumerable.Empty<TimeRecordInfo>(), null, null);
        }

        private TimeRecordsListViewModel PrepareTimeRecords(TimeRecordsListViewModel model,
            IEnumerable<TimeRecordInfo> timeRecords, Guid? selectedProject, Guid? selectedPerson)
        {
            model.SelectedEndDate = DateTime.Now.Date;
            model.SelectedStartDate = DateTime.Now.Date;
            model.Projects = _projectQueryService.GetProjects().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            model.Statuses = this.GetStatuses();

            var projectId = selectedProject == null ? Guid.Parse(model.Projects.First().Value) : (Guid)selectedProject;
            var isUserProjectManager = _userQueryService.IsUserProjectManager(new ProjectId(projectId), new UserId(UserContext.Current.UserId));

            model.IsProjectManager = isUserProjectManager;
            model.People = this.GetPeople(isUserProjectManager, projectId);

            var userReporterId = selectedPerson == null ? Guid.Parse(model.People.First().Value) : (Guid)selectedPerson;
            var userReporter = _userQueryService.GetUser(new UserId(userReporterId));

            model.User = new UserViewModel
            {
                LastName = userReporter.LastName,
                FirstName = userReporter.FirstName
            };

            if (isUserProjectManager)
                timeRecords = timeRecords.Where(x => x.Status == ProjectDomain.Model.DomainLayer.TimeRecordStatus.Notified);

            model.Records = timeRecords.Select(x => new TimeRecordViewModel
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Effort = x.Effort,
                Project = x.Project == null ? null : new ProjectVewModel { Name = x.Project.Name, ProjectId = x.Project.Id },
                TimeRecordId = x.TimeRecordId,
                Status = (TimeRecordStatus)x.Status,
                Task = x.Task == null ? null : new TaskViewModel { TaskId = x.Task.TaskId, Title = x.Task.Title },
                Description = this.TrimString(x.Description, 60),
                UserId = x.UserId,

            }).OrderBy(x => x.StartDate).ToArray();

            return model;
        }

        private IEnumerable<SelectListItem> GetStatuses()
        {
            return new[]
            {
                new SelectListItem
                {
                    Text = TimeRecordStatus.All.ToString(),
                    Value = ((int) TimeRecordStatus.All).ToString(),
                    Selected = true
                },
                new SelectListItem
                {
                    Text = TimeRecordStatus.Open.ToString(),
                    Value = ((int) TimeRecordStatus.Open).ToString()
                },
                new SelectListItem
                {
                    Text = TimeRecordStatus.Notified.ToString(),
                    Value = ((int) TimeRecordStatus.Notified).ToString()
                },
                new SelectListItem
                {
                    Text = TimeRecordStatus.Declined.ToString(),
                    Value = ((int) TimeRecordStatus.Declined).ToString()
                },
                new SelectListItem
                {
                    Text = TimeRecordStatus.Accepted.ToString(),
                    Value = ((int) TimeRecordStatus.Accepted).ToString()
                }
            }.ToList();
        }

        private IEnumerable<SelectListItem> GetPeople(bool isUserProjectManager, Guid projectId)
        {
            if (!isUserProjectManager)
            {
                return this.GetAutorizeUser();
            }

            var userId = new UserId(UserContext.Current.UserId);

            var usersInProject = _userQueryService.GetTeamForProjectManager(userId, new ProjectId(projectId));
            if (usersInProject == null)
            {
                return this.GetAutorizeUser();
            }

            return usersInProject.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.UserId.ToString() });
        }

        private IEnumerable<SelectListItem> GetAutorizeUser()
        {
            return new[]
                {
                    new SelectListItem
                    {
                        Text = UserContext.Current.FirstName + " " + UserContext.Current.LastName,
                        Value = UserContext.Current.UserId.ToString(),
                        Selected = true
                    }
                };
        }

        private string TrimString(string input, int length)
        {
            return input.Length > length ? input.Substring(0, length) + "..." : input;
        }
    }
}