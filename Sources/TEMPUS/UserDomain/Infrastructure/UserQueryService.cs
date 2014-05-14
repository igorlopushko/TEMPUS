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
                UserId = new UserId(x.Id),
                LastName = x.LastName,
                FirstName = x.FirstName,
                Email = x.Email,
                Image = x.Image,
                Password = x.Password,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth,
                Roles = GetUserRoles(new UserId(x.Id))
            }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">user specific email address</param>
        public UserInfo GetUserByEmail(string email)
        {
            var user = _userReadRepository.Users.Where(x => x.Email == email).ToArray().Select(x => new UserInfo
            {
                UserId = new UserId(x.Id),
                LastName = x.LastName,
                FirstName = x.FirstName,
                Email = x.Email,
                Image = x.Image,
                Password = x.Password,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth
            }).FirstOrDefault();
            if (user != null)
            {
                user.Roles = GetUserRoles(user.UserId);
                user.Mood = this.GetUserMood(user.UserId);
                return user;
            }

            return null;
        }

        private IEnumerable<UserRole> GetUserRoles(UserId userId)
        {

            var data = _userReadRepository.UserRoleRelations.Where(x => x.UserId == userId.Id).ToList();
            var result = new List<UserRole>();

            foreach (var userRoleRelation in data)
            {
                var value = new UserRole();
                bool success = Enum.TryParse(userRoleRelation.Role.Name, out value);
                if (success)
                {
                    result.Add(value);
                }
            }

            return result;
        }


        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        public bool ValidateUser(string login, string password)
        {
            var user = _userReadRepository.Users.FirstOrDefault(x => x.Email == login && x.Password == password);
            return user != null;
        }

        /// <summary>
        /// Gets the users roles.
        /// </summary>
        public IEnumerable<KeyValuePair<Guid, string>> GetUsersRoles()
        {
            return _userReadRepository.Roles.AsEnumerable().Select(x => new KeyValuePair<Guid, string>(x.Id, x.Name));
        }


        /// <summary>
        /// Gets the team moods.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <exception cref="System.ArgumentNullException">When projectId is null.</exception>
        public IEnumerable<UserMood> GetTeamMoods(ProjectId projectId)
        {
            if (projectId == null)
                throw new ArgumentNullException("projectId");

            var teamMembersIds =
                _userReadRepository.ProjectRoleRelations.Where(x => x.ProjectId == projectId.Id).Select(x => x.UserId);
            return _userReadRepository.Moods.Where(x => teamMembersIds.Contains(x.UserId)).OrderByDescending(x => x.Date).ToArray().Select(x => new UserMood
                {
                    Date = x.Date,
                    Rate = x.Rate,
                    UserId = new UserId(x.UserId),
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName
                });
        }

        /// <summary>
        /// Gets the user moods.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.ArgumentNullException">When userId is null.</exception>
        public UserMood GetUserMood(UserId userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            var date = DateTime.Now;
            return _userReadRepository.Moods.Where(
                    x => x.UserId == userId.Id && x.Date.Year == date.Year && x.Date.Month == date.Month
                    && x.Date.Day == date.Day).ToArray().Select(x => new UserMood
                        {
                            Date = x.Date,
                            Rate = x.Rate,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            UserId = new UserId(x.UserId)
                        }).
                    FirstOrDefault();
        }


        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <exception cref="System.ArgumentNullException">When id is null.</exception>
        public UserInfo GetUser(UserId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var user = _userReadRepository.Users.Find(id.Id);
            return user == null
                ? null
                : new UserInfo
                {
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Image = user.Image,
                    LastName = user.LastName,
                    Mood = this.GetUserMood(id),
                    Password = user.Password,
                    Phone = user.Phone,
                    UserId = id,
                    Roles = this.GetUserRoles(id)
                };
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        public IEnumerable<UserInfo> GetUsers()
        {
            return _userReadRepository.Users.ToArray().Select(x =>
                    new UserInfo
                        {
                            DateOfBirth = x.DateOfBirth,
                            UserId = new UserId(x.Id),
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Image = x.Image,
                            Mood = this.GetUserMood(new UserId(x.Id)),
                            Password = x.Password,
                            Phone = x.Phone,
                            Roles = this.GetUserRoles(new UserId(x.Id))
                        });
        }
    }
}