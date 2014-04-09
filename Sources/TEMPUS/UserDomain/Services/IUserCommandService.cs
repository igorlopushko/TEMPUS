using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.UserDomain.Services
{
    public interface IUserCommandService : 
        IHandle<CreateUser>
    {
    }
}