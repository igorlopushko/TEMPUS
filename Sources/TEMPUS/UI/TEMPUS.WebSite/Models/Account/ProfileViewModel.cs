using System;
using TEMPUS.BaseDomain.Messages.Identities;

namespace TEMPUS.WebSite.Models.Account
{
    /// <summary>
    /// The class represents extended user information.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public UserId UserId { get; set; }
        /// <summary>
        /// Gets or sets the login of the user.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the image which represents user avatar.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets user's role in project.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user's mood.
        /// </summary>
        public int Mood { get; set; }
    }
}