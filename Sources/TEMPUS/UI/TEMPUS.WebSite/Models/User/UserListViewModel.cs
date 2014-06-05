using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Models.User
{
    public class UserListViewModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserRole { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<SelectListItem> UserRoles { get; set; }
    }
}