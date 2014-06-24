using System;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    [Serializable]
    public class UserMainInfo : Dto
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}