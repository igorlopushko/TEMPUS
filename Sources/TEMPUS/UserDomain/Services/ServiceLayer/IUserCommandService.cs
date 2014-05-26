using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserCommandService : IHandle<CreateUser>,
        IHandle<ChangeUserInformation>,
        IHandle<DeleteUser>,
        IHandle<RestoreUser>,
        IHandle<AddRoleToUser>,
        IHandle<SetUserMood>
    {
    }
}