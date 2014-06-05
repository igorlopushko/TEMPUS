using System;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    /// <summary>
    /// The class represents information related to user activity on the project.
    /// </summary>
    public class UserActivity : Dto
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier of the user.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the fte of the user.
        /// </summary>
        public int FTE { get; set; }

        /// <summary>
        /// Gets or sets the start date of the user activity on the project.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the user activity on the project.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}