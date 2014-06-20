using System;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Services.ServiceLayer;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    public class TimeRecordRepository : Repository<TimeRecord, TimeRecordId>, ITimeRecordRepository
    {
        private readonly ITimeRecordStorage<DB.Models.Project.TimeRecord> timeRecordStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRecordRepository" /> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="timeRecordStorage">The project storage.</param>
        /// <exception cref="System.ArgumentNullException">When projectStorage is null.</exception>
        public TimeRecordRepository(IEventStore eventStore, ITimeRecordStorage<DB.Models.Project.TimeRecord> timeRecordStorage)
            : base(eventStore)
        {
            if (timeRecordStorage == null)
            {
                throw new ArgumentNullException("timeRecordStorage");
            }

            this.timeRecordStorage = timeRecordStorage;
        }

        /// <summary>
        /// Gets the specified time record identifier.
        /// </summary>
        /// <param name="id">The time record identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">When id is null.</exception>
        public override TimeRecord Get(TimeRecordId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var timeRecord = this.timeRecordStorage.Get(id);

            return timeRecord == null
                ? null
                : new TimeRecord(
                    new TimeRecordId(timeRecord.Id),
                    new UserId(timeRecord.UserId),
                    new ProjectId(timeRecord.ProjectId),
                    (TimeRecordStatus)timeRecord.Status,
                    timeRecord.Effort,
                    timeRecord.Description,
                    timeRecord.StartDate,
                    timeRecord.EndDate,
                    timeRecord.IsDeleted);
        }

        /// <summary>
        /// Saves the specified time record aggregate root.
        /// </summary>
        /// <param name="root">The time record aggregate root.</param>
        /// <exception cref="System.ArgumentNullException">When root is null.</exception>
        public void Save(TimeRecord root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            var timeRecordEntity = new DB.Models.Project.TimeRecord
            {
                Description = root.Description,
                Effort = root.Effort,
                EndDate = root.EndDate,
                Id = root.Id.Id,
                IsDeleted = root.IsDeleted,
                ProjectId = root.ProjectId.Id,
                StartDate = root.StartDate,
                Status = (DB.Models.Project.TimeRecordStatus)root.Status,
                UserId = root.UserId.Id
            };

            if (root.IsNew)
            {
                this.timeRecordStorage.Add(timeRecordEntity);
            }
            else
            {
                this.timeRecordStorage.Update(timeRecordEntity);
            }
        }
    }
}