using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.User
{
    /// <summary>
    /// The class represents user entity.
    /// </summary>
    public class User : Entity
    {
        [Key]
        public Guid Id { get; set; }

        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [MaxLength(32)]
        public string Phone { get; set; }

        public string Image { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public IEnumerable<Guid> Roles { get; set; }

        [NotMapped]
        public UserMood Mood { get; set; }
    }
}