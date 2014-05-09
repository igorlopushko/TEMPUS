using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.ServiceLayer;

namespace TEMPUS.ProjectDomain.Services
{
    public interface IProjectQueryService
    {
        IEnumerable<ProjectInfo> GetUserProjects(UserId userid);
    }
}