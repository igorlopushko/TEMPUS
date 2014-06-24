using System;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Model.ServiceLayer
{
    /// <summary>
    /// The class represents information related to time record.
    /// </summary>
    [Serializable]
    public class TimeRecordInfo : Dto
    {
        /// <summary>
        /// Gets or sets the time record identifier.
        /// </summary>
        public Guid TimeRecordId { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        public ProjectInfo Project { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        public TaskInfo Task { get; set; }

        /// <summary>
        /// Gets or sets the effort which user reported.
        /// </summary>
        public double Effort { get; set; }

        /// <summary>
        /// Gets or sets the start date of the time record.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the time record.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the time record.
        /// </summary>
        public TimeRecordStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        public string Description { get; set; }
    }
}