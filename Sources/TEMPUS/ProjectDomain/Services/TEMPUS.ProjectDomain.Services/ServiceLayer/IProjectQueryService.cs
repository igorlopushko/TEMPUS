using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.ServiceLayer;

namespace TEMPUS.ProjectDomain.Services
{
    public interface IProjectQueryService
    {
        IEnumerable<ProjectInfo> GetUserProjects(UserId userId);
    }
}