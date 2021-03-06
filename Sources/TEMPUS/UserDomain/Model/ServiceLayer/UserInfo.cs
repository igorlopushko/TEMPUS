﻿using System;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Model.ServiceLayer
{
    /// <summary>
    /// The class represents extended user information.
    /// </summary>
    [Serializable]
    public class UserInfo : Dto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
        public UserMood Mood { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }
    }
}