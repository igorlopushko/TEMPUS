namespace TEMPUS.WebSite.Models.Account
{
    /// <summary>
    /// The class represents the login information of the user.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the login of the user.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether system should remember user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if system should remember user; otherwise, <c>false</c>.
        /// </value>
        public bool RememberMe { get; set; }
    }
}