using System;

namespace TEMPUS.WebSite.Models.TimeRecord
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The class represents information related to specific time record.
    /// </summary>
    public class TimeRecordViewModel
    {
        /// <summary>
        /// Gets or sets the time record identifier.
        /// </summary>
        public Guid TimeRecordId { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        public ProjectVewModel Project { get; set; }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        public TaskViewModel Task { get; set; }

        /// <summary>
        /// Gets or sets the effort which user reported.
        /// </summary>
        [Required]
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