using System;
using System.Linq;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality for getting User.
    /// </summary>
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserReadRepository _userReadRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryService"/> class.
        /// </summary>
        /// <param name="userReadRepository">The user read storage.</param>
        /// <exception cref="System.ArgumentNullException">When userReadStorage is null</exception>
        public UserQueryService(IUserReadRepository userReadRepository)
        {
            if (userReadRepository == null)
                throw new ArgumentNullException("userReadRepository");

            _userReadRepository = userReadRepository;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserInfo GetUserById(UserId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return _userReadRepository.Get(id);
        }

        /// <summary>
        /// Gets the user by login.
        /// </summary>
        /// <param name="login">The login.</param>
        public UserInfo GetUserByLogin(string login)
        {
            return _userReadRepository.All.FirstOrDefault(x => x.Login == login);
        }

        /// <summary>
        /// Gets the users by project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>Collection of users</returns>
        public IEnumerable<UserInfo> GetUsersByProjectId(ProjectId projectId)
        {
            throw new NotImplementedException();
        }
    }
}