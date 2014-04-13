using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(BaseDomain.Messages.CreateUser msg)
        {
            User user = GetOrCreateUser(msg.Id);
            user.CreateUser(msg.FirstName, msg.LastName);

            _userRepository.Save(user);
        }

        private User GetOrCreateUser(UserId userId)
        {
            User user = _userRepository.Get(userId) ?? new User(userId);
            return user;
        }
    }
}