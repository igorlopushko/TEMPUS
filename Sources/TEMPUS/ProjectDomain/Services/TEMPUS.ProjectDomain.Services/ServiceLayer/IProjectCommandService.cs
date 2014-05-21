using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.ProjectDomain.Services
{
    public interface IProjectCommandService : IHandle<CreateProject>,
        IHandle<ChangeProjectInformation>,
        IHandle<AssignUserToProject>
    {
    }
}