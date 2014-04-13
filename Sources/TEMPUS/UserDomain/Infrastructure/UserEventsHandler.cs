using TEMPUS.BaseDomain.Messages;

namespace TEMPUS.UserDomain.Infrastructure
{
    class UserEventsHandler : IHandle<UserCreated>, IHandle<UserInformationChanged>
    {
        public void Handle(UserInformationChanged @event)
        {
            //TODO Update user information in the repository.
        }

        public void Handle(UserCreated @event)
        {
            //TODO: Save user in the repository.
        }
    }
}