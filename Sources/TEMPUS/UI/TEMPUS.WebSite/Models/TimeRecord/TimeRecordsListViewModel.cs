using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Models.TimeRecord
{
    /// <summary>
    /// The class represents information related user time records.
    /// </summary>
    [Serializable]
    public class TimeRecordsListViewModel
    {
        /// <summary>
        /// Gets or sets the user for this time record.
        /// </summary>
        public UserViewModel User { get; set; }

        /// <summary>
        /// Gets or sets the time records for specified user.
        /// </summary>
        public TimeRecordViewModel[] Records { get; set; }

        /// <summary>
        /// Gets or sets the start date for search.
        /// </summary>
        public DateTime SelectedStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date for search.
        /// </summary>
        public DateTime SelectedEndDate { get; set; }

        /// <summary>
        /// Gets or sets the selected project.
        /// </summary>
        public Guid SelectedProject { get; set; }

        /// <summary>
        /// Gets or sets the selected status.
        /// </summary>
        public TimeRecordStatus SelectedStatus { get; set; }

        public IEnumerable<SelectListItem> Projects { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}