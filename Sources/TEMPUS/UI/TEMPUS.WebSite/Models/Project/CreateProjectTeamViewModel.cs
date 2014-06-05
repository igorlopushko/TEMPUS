using System.Collections.Generic;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Models.Project
{
    public class CreateProjectTeamViewModel
    {
        public IEnumerable<ProjectTeamMemberViewModel> TeamMembers { get; set; }
        public IEnumerable<ProjectTeamMemberViewModel> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}