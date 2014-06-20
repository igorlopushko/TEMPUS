using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Services;
using TEMPUS.ProjectDomain.Services.ServiceLayer;
using TEMPUS.WebSite.Contexts;
using TEMPUS.WebSite.Models.TimeRecord;

namespace TEMPUS.WebSite.Controllers
{
    /// <summary>
    /// The class represents functionality related to time records.
    /// </summary>
    public class TimeReportController : Controller
    {
        private readonly ICommandSender _commandSender;
        private readonly ITimeRecordQueryService _timeRecordQueryService;
        private readonly IProjectQueryService _projectQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeReportController" /> class.
        /// </summary>
        /// <param name="commandSender">The sender.</param>
        /// <param name="timeRecordQueryService">The query service.</param>
        /// <param name="projectQueryService">The project query service.</param>
        /// <exception cref="System.ArgumentNullException">When commandSender or timeRecordQueryService or projectQueryService are null.</exception>
        public TimeReportController(ICommandSender commandSender, ITimeRecordQueryService timeRecordQueryService,
            IProjectQueryService projectQueryService)
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

            _commandSender = commandSender;
            _timeRecordQueryService = timeRecordQueryService;
            _projectQueryService = projectQueryService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var userId = new UserId(UserContext.Current.UserId);
            var timeRecords = _timeRecordQueryService.GetUserTimeRecords(userId);
            var model = new TimeRecordsListViewModel();

            return View(this.PrepareTimeRecords(model, timeRecords));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(TimeRecordsListViewModel model)
        {
            var userId = new UserId(UserContext.Current.UserId);
            var timeRecords = _timeRecordQueryService.GetUserTimeRecords(userId)
                .Where(x => x.StartDate >= model.SelectedStartDate
                    && x.EndDate <= model.SelectedEndDate
                    && x.Project.Id == model.SelectedProject);

            if (model.SelectedStatus != TimeRecordStatus.All)
                timeRecords = timeRecords.Where(x => x.Status == (ProjectDomain.Model.DomainLayer.TimeRecordStatus)model.SelectedStatus);

            return View(this.PrepareTimeRecords(model, timeRecords.ToList()));
        }

        [HttpPost]
        public ActionResult Create(TimeRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
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

        private TimeRecordsListViewModel PrepareTimeRecords(TimeRecordsListViewModel model,
            IEnumerable<TimeRecordInfo> timeRecords)
        {
            model.SelectedEndDate = DateTime.Now.Date;
            model.SelectedStartDate = DateTime.Now.Date;
            model.Projects = _projectQueryService.GetProjects().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            model.Statuses = this.GetStatuses();
            model.User = new UserViewModel
            {
                LastName = UserContext.Current.LastName,
                FirstName = UserContext.Current.FirstName
            };
            model.Records = timeRecords.Select(x => new TimeRecordViewModel
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Effort = x.Effort,
                Project = x.Project == null ? null : new ProjectVewModel { Name = x.Project.Name, ProjectId = x.Project.Id },
                TimeRecordId = x.TimeRecordId,
                Status = (TimeRecordStatus)x.Status,
                Task = x.Task == null ? null : new TaskViewModel { TaskId = x.Task.TaskId, Title = x.Task.Title },
                Description = this.TrimString(x.Description, 60)
            }).OrderBy(x => x.StartDate);

            return model;
        }

        private IEnumerable<SelectListItem> GetStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text = TimeRecordStatus.All.ToString(), Value = ((int)TimeRecordStatus.All).ToString(), Selected = true},
                new SelectListItem{Text = TimeRecordStatus.Open.ToString(), Value = ((int)TimeRecordStatus.Open).ToString()},
                new SelectListItem{Text = TimeRecordStatus.Notified.ToString(), Value = ((int)TimeRecordStatus.Notified).ToString()},
                new SelectListItem{Text = TimeRecordStatus.Declined.ToString(), Value = ((int)TimeRecordStatus.Declined).ToString()},
                new SelectListItem{Text = TimeRecordStatus.Accepted.ToString(), Value = ((int)TimeRecordStatus.Accepted).ToString()}
            }.ToList();
        }

        private string TrimString(string input, int length)
        {
            return input.Length > length ? input.Substring(0, length) + "..." : input;
        }
    }
}