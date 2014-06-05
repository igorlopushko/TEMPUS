using System;
using System.Collections.Generic;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectTeamMemberViewModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<ProjectTeamMemberActivityViewModel> TeamMemberActivities { get; set; }
    }
}