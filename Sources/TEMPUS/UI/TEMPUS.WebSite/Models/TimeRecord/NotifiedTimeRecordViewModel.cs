using System;

namespace TEMPUS.WebSite.Models.TimeRecord
{
    /// <summary>
    /// The class represents time records which is notified or not.
    /// </summary>
    public class NotifiedTimeRecordViewModel
    {
        /// <summary>
        /// Gets or sets the time record identifier.
        /// </summary>
        public Guid TimeRecordId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether time record is notified.
        /// </summary>
        public bool IsNotified { get; set; }
    }
}