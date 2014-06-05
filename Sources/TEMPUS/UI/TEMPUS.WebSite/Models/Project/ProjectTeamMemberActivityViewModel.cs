using System;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectTeamMemberActivityViewModel
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public int FTE { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}