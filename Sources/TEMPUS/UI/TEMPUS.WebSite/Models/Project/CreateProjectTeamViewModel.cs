﻿using System.Collections.Generic;
using System.Web.Mvc;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.WebSite.Models.Project
{
    public class CreateProjectTeamViewModel
    {
        public IEnumerable<ProjectTeamMemberViewModel> TeamMembers { get; set; }
        public IEnumerable<UserInfo> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}