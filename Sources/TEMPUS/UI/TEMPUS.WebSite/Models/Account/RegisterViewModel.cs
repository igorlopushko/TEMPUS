using System;
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
        [MaxLength(50, ErrorMessage = "Email field length must be less than 50 symbols.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email field should be like Test@gmail.com")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [MinLength(6, ErrorMessage = "Password length must be more than 6 symbols.")]
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
        [Required]
        [MaxLength(50, ErrorMessage = "Length on the FirstName field need to be less than 50 symbols.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the role of the new user.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the new user.
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}