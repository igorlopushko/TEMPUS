using TEMPUS.UserDomain.Services;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserCommandService : IUserCommandService
    {
        public void Handle(BaseDomain.Messages.CreateUser msg)
        {
            //TODO
        }
    }
}