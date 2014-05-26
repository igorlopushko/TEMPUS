using System;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectTeamMemberViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        public int FTE { get; set; }
    }
}