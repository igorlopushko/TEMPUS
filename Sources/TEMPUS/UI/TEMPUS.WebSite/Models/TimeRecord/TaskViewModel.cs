using System;

namespace TEMPUS.WebSite.Models.TimeRecord
{
    /// <summary>
    /// The class represents information related to specific task.
    /// </summary>
    public class TaskViewModel
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Gets or sets the title of the task.
        /// </summary>
        public string Title { get; set; }
    }
}