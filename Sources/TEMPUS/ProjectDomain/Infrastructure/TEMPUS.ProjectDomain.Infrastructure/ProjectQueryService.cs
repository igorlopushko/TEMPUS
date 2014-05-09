using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMPUS.ProjectDomain.Services;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    public class ProjectQueryService : IProjectQueryService
    {
        public IEnumerable<Model.ServiceLayer.ProjectInfo> GetUserProjects(BaseDomain.Messages.Identities.UserId userid)
        {
            throw new NotImplementedException();
        }
    }
}