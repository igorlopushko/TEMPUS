using System;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class TimeRecord : AggregateRoot<TimeRecordId>
    {
        private readonly TimeRecordId _id;

        public UserId UserId { get; private set; }
        public ProjectId ProjectId { get; private set; }
        public TimeRecordStatus Status { get; private set; }
        public double Effort { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public override TimeRecordId Id
        {
            get { return _id; }
        }

        public bool IsNew { get; private set; }
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRecord"/> class.
        /// </summary>
        /// <param name="id">The time record identifier.</param>
        public TimeRecord(TimeRecordId id)
        {
            this._id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRecord" /> class.
        /// </summary>
        /// <param name="id">The time record identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="status">The status of the rime record.</param>
        /// <param name="effort">The effort of the rime record.</param>
        /// <param name="description">The description of the rime record.</param>
        /// <param name="startDate">The start date of the rime record.</param>
        /// <param name="endDate">The end date of the rime record.</param>
        /// <param name="isDeleted">if set to <c>true</c> time report is deleted, otherwise <c>false</c>.</param>
        public TimeRecord(TimeRecordId id, UserId userId, ProjectId projectId, TimeRecordStatus status, double effort,
            string description, DateTime startDate, DateTime endDate, bool isDeleted)
        {
            this._id = id;
            this.UserId = userId;
            this.ProjectId = projectId;
            this.Status = status;
            this.Effort = effort;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.IsDeleted = IsDeleted;
            this.IsNew = false;
        }

        /// <summary>
        /// Creates the time record.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="effort">The effort of the time record.</param>
        /// <param name="description">The description of the time record.</param>
        /// <param name="startDate">The start date of the time record.</param>
        /// <param name="endDate">The end date of the time record.</param>
        public void CreateTimeRecord(UserId userId, ProjectId projectId, double effort, string description,
            DateTime startDate, DateTime endDate)
        {
            this.UserId = userId;
            this.ProjectId = projectId;
            this.Status = TimeRecordStatus.Open;
            this.Effort = effort;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.IsNew = true;
            this.IsDeleted = false;
        }

        /// <summary>
        /// Notifies the time record.
        /// </summary>
        public void NotifiedTimeRecord()
        {
            this.Status = TimeRecordStatus.Notified;
        }

        /// <summary>
        /// Accepts the time record.
        /// </summary>
        public void AcceptTimeRecord()
        {
            this.Status = TimeRecordStatus.Accepted;
        }

        /// <summary>
        /// Declines the time record.
        /// </summary>
        public void DeclineTimeRecord()
        {
            this.Status = TimeRecordStatus.Declined;
        }

        /// <summary>
        /// Deletes the time report.
        /// </summary>
        public void DeleteTimeReport()
        {
            this.IsDeleted = true;
        }
    }
}
