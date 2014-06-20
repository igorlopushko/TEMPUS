using System;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Services.ServiceLayer;

namespace TEMPUS.ProjectDomain.Services
{
    public class TimeRecordCommandService : ITimeRecordCommandService
    {
        private readonly ITimeRecordRepository timeRecordRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRecordCommandService"/> class.
        /// </summary>
        /// <param name="timeRecordRepository">The project storage.</param>
        /// <exception cref="System.ArgumentNullException">When projectStorage is null.</exception>
        public TimeRecordCommandService(ITimeRecordRepository timeRecordRepository)
        {
            if (timeRecordRepository == null)
            {
                throw new ArgumentNullException("timeRecordRepository");
            }

            this.timeRecordRepository = timeRecordRepository;
        }

        /// <summary>
        /// Handles the specified <see cref="CreateTimeRecord"/>command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When TimeRecordId is null.</exception>
        public void Handle(CreateTimeRecord command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("TimeRecordId", "TimeRecordId must be specified.");
            }

            var timeRecord = this.GetOrCreateTimeRecord(command.Id);

            timeRecord.CreateTimeRecord(command.UserId, command.ProjectId, command.Effort, command.Description,
                command.StartDate, command.EndDate);

            this.timeRecordRepository.Save(timeRecord);
        }

        /// <summary>
        /// Handles the specified <see cref="NotifyTimeRecord"/>command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When TimeRecordId is null.</exception>
        public void Handle(NotifyTimeRecord command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("TimeRecordId", "TimeRecordId must be specified.");
            }

            var timeRecord = this.timeRecordRepository.Get(command.Id);

            timeRecord.NotifyTimeRecord();

            this.timeRecordRepository.Save(timeRecord);
        }

        /// <summary>
        /// Handles the specified <see cref="AcceptTimeRecord"/>command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When TimeRecordId is null.</exception>
        public void Handle(AcceptTimeRecord command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("TimeRecordId", "TimeRecordId must be specified.");
            }

            var timeRecord = this.timeRecordRepository.Get(command.Id);

            timeRecord.AcceptTimeRecord();

            this.timeRecordRepository.Save(timeRecord);
        }

        /// <summary>
        /// Handles the specified <see cref="DeclineTimeRecord"/>command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When TimeRecordId is null.</exception>
        public void Handle(DeclineTimeRecord command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("TimeRecordId", "TimeRecordId must be specified.");
            }

            var timeRecord = this.timeRecordRepository.Get(command.Id);

            timeRecord.DeclineTimeRecord();

            this.timeRecordRepository.Save(timeRecord);
        }

        /// <summary>
        /// Handles the specified <see cref="DeleteTimeReport"/>command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When TimeRecordId is null.</exception>
        public void Handle(DeleteTimeReport command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("TimeRecordId", "TimeRecordId must be specified.");
            }

            var timeRecord = this.timeRecordRepository.Get(command.Id);

            timeRecord.DeleteTimeReport();

            this.timeRecordRepository.Save(timeRecord);
        }

        private TimeRecord GetOrCreateTimeRecord(TimeRecordId id)
        {
            var project = this.timeRecordRepository.Get(id);
            if (project != null)
                throw new ArgumentException(string.Format("Time record with such id: {0} exist in the system.", id.Id));

            return new TimeRecord(id);
        }
    }
}