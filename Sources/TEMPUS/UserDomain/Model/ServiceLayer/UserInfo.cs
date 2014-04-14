using System;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    /// <summary>
    /// The class represents extended user information.
    /// </summary>
    [Serializable]
    public class UserInfo : Dto
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
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the phone of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the image which represents user avatar.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the user's feelings
        /// </summary>
        public string Feelings { get; set; }

        /// <summary>
        /// Gets or sets user's role in the project
        /// </summary>
        public string Role { get; set; }

        //TODO Add properties if needed.
    }
}