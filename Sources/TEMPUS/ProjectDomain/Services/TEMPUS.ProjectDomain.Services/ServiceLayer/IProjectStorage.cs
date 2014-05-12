using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.DB.Models;

namespace TEMPUS.ProjectDomain.Services
{
    public interface IProjectStorage<T> : IStorage<T, ProjectId>
        where T : Entity
    {
    }
}