using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEMPUS.WebSite.Models.Team
{
    /// <summary>
    /// The class represents user information.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the phone of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the image which represents user avatar.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets user's role in project.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets user's email.
        /// </summary>
        public string Email { get; set; }
    }
}