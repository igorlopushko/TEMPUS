using System;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    /// <summary>
    /// The class represents information related to project role.
    /// </summary>
    public class ProjectRole : Dto
    {
        /// <summary>
        /// Gets or sets the identifier of the project role.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project role.
        /// </summary>
        public string Name { get; set; }
    }
}