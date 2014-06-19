using TEMPUS.BaseDomain.Messages.Identities;

namespace TEMPUS.WebSite.Models.TimeRecord
{
    /// <summary>
    /// The class represents information related to specific project.
    /// </summary>
    public class ProjectVewModel
    {
        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        public ProjectId ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }
    }
}