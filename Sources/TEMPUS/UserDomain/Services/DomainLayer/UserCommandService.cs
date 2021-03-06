﻿using System;
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

            user.ChangeInformation(command.FirstName, command.LastName, command.Image, command.Phone, command.DateOfBirth);

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
        /// Handles the specified <see cref="RestoreUser"/> command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When UserId is null.</exception>
        /// <exception cref="System.ArgumentException">When user does not exist.</exception>
        public void Handle(RestoreUser command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("command", "UserId must be specified.");

            var user = _userRepository.Get(command.Id);
            if (user == null)
                throw new ArgumentException(string.Format("User with such id: {0} does not exist.", command.Id));

            user.RestoreUser();

            _userRepository.Save(user);
        }

        /// <summary>
        /// Handles the specified <see cref="AddRoleToUser"/> command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When UserId is null.</exception>
        /// <exception cref="System.ArgumentException">When user does not exist.</exception>
        public void Handle(AddRoleToUser command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("command", "UserId must be specified.");

            var user = _userRepository.Get(command.Id);
            if (user == null)
                throw new ArgumentException(string.Format("User with such id: {0} does not exist.", command.Id));

            user.AddRole(command.RoleId);

            _userRepository.Save(user);
        }

        /// <summary>
        /// Handles the specified <see cref="SetUserMood"/> command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Handle(SetUserMood command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("command", "UserId must be specified.");

            var user = _userRepository.Get(command.Id);
            if (user == null)
                throw new ArgumentException(string.Format("User with such id: {0} does not exist.", command.Id));

            if (user.Mood != null)
                throw new ArgumentException("User can set his mood only one time per day.");

            var date = DateTime.Now.Date;
            user.AddMood(command.Rate, date);

            _userRepository.Save(user);
        }

        /// <summary>
        /// Gets the or create user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        private User GetOrCreateUser(UserId userId)
        {
            var user = _userRepository.Get(userId);
            if(user != null)
                throw new ArgumentException(string.Format("User with such id: {0} exist in the system."));

            return new User(userId);
        }
    }
}