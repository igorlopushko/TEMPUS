using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.DB.Models.Project;
using TEMPUS.ProjectDomain.Services.ServiceLayer;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    public class TimeRecordStorage : ITimeRecordStorage<TimeRecord>
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRecordStorage"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">When context is null.</exception>
        public TimeRecordStorage(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The time record identifier.</param>
        /// <exception cref="System.ArgumentNullException">When id is null.</exception>
        public TimeRecord Get(TimeRecordId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return this.context.TimeRecords.Find(id.Id);
        }

        /// <summary>
        /// Gets the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<TimeRecord> Get(Expression<Func<TimeRecord, bool>> expression)
        {
            return this.context.TimeRecords.Where(expression);
        }

        /// <summary>
        /// Adds the specified time record entity.
        /// </summary>
        /// <param name="entity">The time record entity.</param>
        /// <exception cref="System.ArgumentNullException">When entity is null.</exception>
        public void Add(TimeRecord entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.context.TimeRecords.Add(entity);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified time record entity.
        /// </summary>
        /// <param name="entity">The time record entity.</param>
        /// <exception cref="System.ArgumentNullException">When entity is null.</exception>
        public void Delete(TimeRecord entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.context.TimeRecords.Remove(entity);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified time record aggregate.
        /// </summary>
        /// <param name="aggregate">The time record aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When aggregate is null</exception>
        public void Update(TimeRecord aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException("aggregate");
            }

            var timeRecord = this.context.TimeRecords.Find(aggregate.Id);
            timeRecord.IsDeleted = aggregate.IsDeleted;
            timeRecord.StartDate = aggregate.StartDate;
            timeRecord.EndDate = aggregate.EndDate;
            timeRecord.Description = aggregate.Description;
            timeRecord.Effort = aggregate.Effort;
            timeRecord.ProjectId = aggregate.ProjectId;
            timeRecord.UserId = aggregate.UserId;
            timeRecord.Status = aggregate.Status;

            this.context.SaveChanges();
        }
    }
}