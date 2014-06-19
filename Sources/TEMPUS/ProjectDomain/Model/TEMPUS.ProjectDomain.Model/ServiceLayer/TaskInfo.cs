using System;

namespace TEMPUS.ProjectDomain.Model.ServiceLayer
{
    /// <summary>
    /// The class represents information related to task.
    /// </summary>
    [Serializable]
    public class TaskInfo
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