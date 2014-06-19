using System;
using System.Collections.Generic;
using System.Linq;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Services.ServiceLayer;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality related to time records.
    /// </summary>
    public class TimeRecordQueryService : ITimeRecordQueryService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRecordQueryService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">When context is null.</exception>
        public TimeRecordQueryService(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        /// <summary>
        /// Gets the user time records.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentNullException">When id is null.</exception>
        public IEnumerable<TimeRecordInfo> GetUserTimeRecords(UserId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return _context.TimeRecords.Where(x => x.UserId == id.Id).AsEnumerable().Select(x => new TimeRecordInfo
            {
                Status = (TimeRecordStatus)x.Status,
                Effort = x.Effort,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                TimeRecordId = x.Id,
                Project = new ProjectInfo(x.Project.Id, x.Project.Name)
            });
        }
    }
}