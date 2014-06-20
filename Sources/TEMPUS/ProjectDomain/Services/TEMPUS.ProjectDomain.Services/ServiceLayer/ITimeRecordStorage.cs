using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.DB.Models;

namespace TEMPUS.ProjectDomain.Services.ServiceLayer
{
    public interface ITimeRecordStorage<T> : IStorage<T, TimeRecordId>
        where T : Entity
    {
    }
}