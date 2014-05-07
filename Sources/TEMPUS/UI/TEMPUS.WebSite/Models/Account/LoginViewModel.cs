using System;
using System.ComponentModel.DataAnnotations;

namespace TEMPUS.WebSite.Models.Account
{
    /// <summary>
    /// The class represents the login information of the user.
    /// </summary>
    [Serializable]
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the login of the user.
        /// </summary>
        [Required]
        [MaxLength(50,ErrorMessage = "Email field length must be less than 50 symbols.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email field should be like Test@gmail.com")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [MinLength(6, ErrorMessage = "Password length must be more than 6 symbols.")]
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