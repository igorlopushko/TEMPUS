using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.ServiceLayer;

namespace TEMPUS.ProjectDomain.Services.ServiceLayer
{
    public interface ITimeRecordQueryService
    {
        IEnumerable<TimeRecordInfo> GetUserTimeRecords(UserId id);
    }
}