using System;
using TEMPUS.BaseDomain.Messages.Identities;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class TeamMember
    {
        public UserId UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}