using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserCommandService : 
        IHandle<CreateUser>
    {
    }
}