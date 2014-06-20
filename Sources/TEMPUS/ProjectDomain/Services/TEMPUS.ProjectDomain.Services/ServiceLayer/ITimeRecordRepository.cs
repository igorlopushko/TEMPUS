using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Services.ServiceLayer
{
    public interface ITimeRecordRepository : IRepository<TimeRecord, TimeRecordId>
    {
    }
}