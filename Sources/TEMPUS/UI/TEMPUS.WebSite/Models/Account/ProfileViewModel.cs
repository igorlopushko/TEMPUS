namespace TEMPUS.WebSite.Models.Account
{
    /// <summary>
    /// The class represents extended user information.
    /// </summary>
    public class ProfileViewModel
    {
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

        //TODO Add properties if needed.
    }
}