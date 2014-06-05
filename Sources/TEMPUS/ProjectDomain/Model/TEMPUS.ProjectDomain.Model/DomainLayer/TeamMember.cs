using System;
using TEMPUS.BaseDomain.Messages.Identities;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    /// <summary>
    /// The class represents information related to team member on the project.
    /// </summary>
    public class TeamMember
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public UserId UserId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier of the user.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the start date, when team member start working on the project.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date, when team member stop working on the project.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the fte of the user.
        /// </summary>
        public int FTE { get; set; }
    }
}