using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Services
{
    public interface IProjectRepository : IRepository<Project, ProjectId>
    {
    }
}