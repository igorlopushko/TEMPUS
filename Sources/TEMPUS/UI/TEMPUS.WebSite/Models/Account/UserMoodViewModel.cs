using System;

namespace TEMPUS.WebSite.Models.Account
{
    [Serializable]
    public class UserMoodViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the rate of the mood.
        /// </summary>
        public int Rate { get; set; }
    }
}