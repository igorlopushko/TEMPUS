using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Models.Account
{
    /// <summary>
    /// The class represents register information of the user.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the login of the user.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Password need to be equals.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }
    }
}