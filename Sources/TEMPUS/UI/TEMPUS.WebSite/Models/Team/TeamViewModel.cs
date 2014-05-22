using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.WebSite.Models.Account;

namespace TEMPUS.WebSite.Models.Team
{
    public class TeamViewModel
    {
        public ProjectId projectId { get; set; }
        public List<ProfileViewModel> users { get; set; }
    }
}