using System;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Services.DomainLayer
{
    /// <summary>
    /// The class represents service for handling commands related to user.
    /// </summary>
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommandService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserCommandService(IUserRepository userRepository)
        {
            if (userRepository == null)
                throw new ArgumentNullException("userRepository");

            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the specified <see cref="CreateUser">command</see>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When UserId is null.</exception>
        /// <exception cref="System.ArgumentException">When user login is null or empty or contains only white spaces.</exception>
        public void Handle(CreateUser command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("command", "UserId must be specified.");

            if (string.IsNullOrWhiteSpace(command.Login))
                throw new ArgumentException("User login must be specified.");

            User user = GetOrCreateUser(command.Id);
            user.CreateUser(command.Login, command.Password);

            _userRepository.Save(user);
        }

        /// <summary>
        /// Handles the specified <see cref="ChangeUserInformation">command</see>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When UserId is null.</exception>
        /// <exception cref="System.ArgumentException">When user does not exist.</exception>
        public void Handle(ChangeUserInformation command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("command", "UserId must be specified.");

            User user = _userRepository.Get(command.Id);
            if (user == null)
                throw new ArgumentException(string.Format("User with such id: {0} does not exist.", command.Id));

            user.ChangeInformation(command.Password, command.FirstName, command.LastName, command.Age, command.Image,
                command.Phone);

            _userRepository.Save(user);
        }

        /// <summary>
        /// Handles the specified <see cref="DeleteUser">command</see>.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When UserId is null.</exception>
        /// <exception cref="System.ArgumentException">When user does not exist.</exception>
        public void Handle(DeleteUser command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("command", "UserId must be specified.");

            var user = _userRepository.Get(command.Id);
            if (user == null)
                throw new ArgumentException(string.Format("User with such id: {0} does not exist.", command.Id));

            user.DeleteUser();

            _userRepository.Save(user);
        }

        /// <summary>
        /// Gets the or create user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private User GetOrCreateUser(UserId userId)
        {
            return _userRepository.Get(userId) ?? new User(userId);
        }
    }
}