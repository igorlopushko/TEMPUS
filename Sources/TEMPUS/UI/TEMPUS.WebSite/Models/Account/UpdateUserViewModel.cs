namespace TEMPUS.WebSite.Models.Account
{
    /// <summary>
    /// The model represents information for the user updating operation.
    /// </summary>
    public class UpdateUserViewModel
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
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the new age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the new phone of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the new image which represents user avatar.
        /// </summary>
        public string Image { get; set; }

        //TODO Add properties if needed.
    }
}