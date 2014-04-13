using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Services.DomainLayer
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creates new user aggregate root and saves with reposiroty.
        /// </summary>
        /// <param name="msg">Incomming message</param>
        public void Handle(CreateUser msg)
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