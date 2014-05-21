﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Models.Project
{
    public class CreateProjectViewModel
    {
        public CreateProjectMainInfoViewModel ProjectMainInfo { get; set; }

        public CreateProjectTeamViewModel ProjectTeam { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}