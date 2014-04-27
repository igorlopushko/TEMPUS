using System;
using System.Collections.Generic;
using System.Linq;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality for getting User.
    /// </summary>
    public class UserQueryService : IUserQueryService
    {
        private readonly DataContext _userReadRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryService"/> class.
        /// </summary>
        /// <param name="userReadRepository">The user read storage.</param>
        /// <exception cref="System.ArgumentNullException">When userReadStorage is null</exception>
        public UserQueryService(DataContext userReadRepository)
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

            return _userReadRepository.Users.Where(x => x.Id == id.Id).AsEnumerable().Select(x => new UserInfo
            {
                Age = x.Age,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Image = x.Image,
                Login = x.Login,
                Password = x.Password,
                Phone = x.Phone,
                UserId = new UserId(x.Id),
                Role = x.Role
            }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by login.
        /// </summary>
        /// <param name="login">The login.</param>
        public UserInfo GetUserByLogin(string login)
        {
            return _userReadRepository.Users.Where(x => x.Login == login).AsEnumerable().Select(x => new UserInfo
            {
                Age = x.Age,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Image = x.Image,
                Login = x.Login,
                Password = x.Password,
                Phone = x.Phone,
                UserId = new UserId(x.Id),
                Role = x.Role
            }).FirstOrDefault();
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