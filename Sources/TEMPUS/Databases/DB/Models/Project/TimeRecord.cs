using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.Project
{
    /// <summary>
    /// The class represents information related to time that user reports to specified project.
    /// </summary>
    public class TimeRecord : Entity
    {
        /// <summary>
        /// Gets or sets the time record identifier.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user which is reported the time.
        /// </summary>
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the status of the specified record.
        /// </summary>
        public TimeRecordStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the effort which user reported.
        /// </summary>
        public double Effort { get; set; }

        /// <summary>
        /// Gets or sets the description of the record.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date of the time record.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the record.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the user which is reported the time.
        /// </summary>
        public virtual User.User User { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
