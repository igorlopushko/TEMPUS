using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.ProjectDomain.Services
{
    public interface ITimeRecordCommandService : IHandle<CreateTimeRecord>, 
        IHandle<NotifyTimeRecord>, 
        IHandle<AcceptTimeRecord>, 
        IHandle<DeclineTimeRecord>, 
        IHandle<DeleteTimeReport>
    {
    }
}