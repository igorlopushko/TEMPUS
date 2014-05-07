﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEMPUS.DB.Models.User
{
    /// <summary>
    /// The class represents user entity.
    /// </summary>
    public class User : Entity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
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
    }
}