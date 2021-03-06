﻿using System;
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
        private const string managerRoleName = "Manager";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserQueryService"/> class.
        /// </summary>
        /// <param name="userReadRepository">The user read storage.</param>
        /// <exception cref="System.ArgumentNullException">When userReadStorage is null</exception>
        public UserQueryService(DataContext userReadRepository)
        {
            if (userReadRepository == null)
            {
                throw new ArgumentNullException("userReadRepository");
            }

            _userReadRepository = userReadRepository;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserInfo GetUserById(UserId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return _userReadRepository.Users.Where(x => x.Id == id.Id && x.IsDeleted == false).AsEnumerable().Select(x => new UserInfo
            {
                UserId = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Email = x.Email,
                Image = x.Image,
                Password = x.Password,
                Phone = x.Phone,
                Mood = this.GetUserMood(new UserId(x.Id)),
                DateOfBirth = x.DateOfBirth,
                IsDeleted = x.IsDeleted,
                Roles = GetUserRoles(new UserId(x.Id))
            }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">user specific email address</param>
        public UserInfo GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email must be specified.");
            }

            var user = _userReadRepository.Users.Where(x => x.Email == email && x.IsDeleted == false).ToArray().Select(x => new UserInfo
            {
                UserId = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Email = x.Email,
                Image = x.Image,
                Password = x.Password,
                Phone = x.Phone,
                Mood = this.GetUserMood(new UserId(x.Id)),
                IsDeleted = x.IsDeleted,
                DateOfBirth = x.DateOfBirth
            }).FirstOrDefault();
            if (user != null)
            {
                user.Roles = GetUserRoles(new UserId(user.UserId));
                user.Mood = this.GetUserMood(new UserId(user.UserId));
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
            var user = _userReadRepository.Users.FirstOrDefault(x => x.Email == login && x.Password == password && x.IsDeleted == false);
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
            {
                throw new ArgumentNullException("projectId");
            }

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
        /// Gets the users by project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <exception cref="System.ArgumentNullException">projectId</exception>
        public IEnumerable<UserInfo> GetUsersByProjectId(ProjectId projectId)
        {
            if (projectId == null)
            {
                throw new ArgumentNullException("projectId");
            }

            var teamMembersIds =
                _userReadRepository.ProjectRoleRelations.Where(x => x.ProjectId == projectId.Id).Select(x => x.UserId);

            var users = _userReadRepository.Users.Where(x => teamMembersIds.Contains(x.Id)).ToArray().Select(x => new UserInfo
            {
                UserId = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Email = x.Email,
                Image = x.Image,
                Password = x.Password,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth,
                IsDeleted = x.IsDeleted,
                Mood = this.GetUserMood(new UserId(x.Id)),
                Roles = this.GetUserRoles(new UserId(x.Id))
            });

            return users;
        }

        /// <summary>
        /// Gets the project role for the specified user.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public ProjectRole GetProjectRoleForUser(ProjectId projectId, UserId userId)
        {
            if (projectId == null)
            {
                throw new ArgumentNullException("projectId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            return _userReadRepository.ProjectRoleRelations
                .Where(x => x.ProjectId == projectId.Id && x.UserId == userId.Id)
                .Select(x => new ProjectRole
                    {
                        Id = x.ProjectRole.Id,
                        Name = x.ProjectRole.Name
                    }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the user moods.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.ArgumentNullException">When userId is null.</exception>
        public UserMood GetUserMood(UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            var date = DateTime.Now.Date;
            return _userReadRepository.Moods.Where(
                    x => x.UserId == userId.Id && x.Date == date).ToArray().Select(x => new UserMood
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

            var user = _userReadRepository.Users.FirstOrDefault(x => x.Id == id.Id && x.IsDeleted == false);
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
                    UserId = id.Id,
                    IsDeleted = user.IsDeleted,
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
                            UserId = x.Id,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Image = x.Image,
                            Mood = this.GetUserMood(new UserId(x.Id)),
                            Password = x.Password,
                            Phone = x.Phone,
                            IsDeleted = x.IsDeleted,
                            Roles = this.GetUserRoles(new UserId(x.Id))
                        });
        }


        /// <summary>
        /// Gets all active users.
        /// </summary>
        public IEnumerable<UserInfo> GetAllActiveUsers()
        {
            var administratorRole = _userReadRepository.Roles.FirstOrDefault(x => x.Name == UserRole.Administrator.ToString());
            if (administratorRole == null)
                return null;

            var administrators = _userReadRepository.UserRoleRelations.Where(x => x.RoleId == administratorRole.Id).Select(x => x.UserId).ToArray();

            return _userReadRepository.Users.Where(x => x.IsDeleted == false && !administrators.Contains(x.Id)).ToArray().Select(x =>
                    new UserInfo
                    {
                        DateOfBirth = x.DateOfBirth,
                        UserId = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Image = x.Image,
                        Mood = this.GetUserMood(new UserId(x.Id)),
                        Password = x.Password,
                        Phone = x.Phone,
                        IsDeleted = x.IsDeleted,
                        Roles = this.GetUserRoles(new UserId(x.Id))
                    });
        }

        /// <summary>
        /// Gets the user role identifier.
        /// </summary>
        /// <param name="role">The role.</param>
        public Guid GetUserRoleId(UserRole role)
        {
            var userRole = _userReadRepository.Roles.FirstOrDefault(x => x.Name == role.ToString());
            return userRole == null ? Guid.Empty : userRole.Id;
        }


        /// <summary>
        /// Gets the user activities.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<UserActivity> GetUserActivities(UserId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var date = DateTime.Now.Date;

            return _userReadRepository.ProjectRoleRelations
                    .Where(x => x.UserId == id.Id && x.EndDate > date)
                    .Select(x => new UserActivity
                                    {
                                        RoleId = x.ProjectRoleId,
                                        FTE = x.FTE,
                                        EndDate = x.EndDate,
                                        StartDate = x.StartDate,
                                        UserId = x.UserId
                                    });
        }


        /// <summary>
        /// Gets the team for project manager.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <exception cref="System.ArgumentNullException">When id or projectId are null.</exception>
        public IEnumerable<UserMainInfo> GetTeamForProjectManager(UserId id, ProjectId projectId)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (projectId == null)
            {
                throw new ArgumentNullException("projectId");
            }

            var teamMembers = _userReadRepository.ProjectRoleRelations
                .Where(x => x.ProjectId == projectId.Id);

            var isUserProjectManager = teamMembers
                .FirstOrDefault(x => x.UserId == id.Id && x.ProjectRole.Name == managerRoleName) != null;

            if (!isUserProjectManager)
                return null;

            return _userReadRepository.Users.Where(x => teamMembers.Select(user => user.UserId).Contains(x.Id))
                    .Select(x => new UserMainInfo
                    {
                        UserId = x.Id,
                        LastName = x.LastName,
                        FirstName = x.FirstName
                    });
        }


        /// <summary>
        /// Determines whether user is project manager on the specified project.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="System.ArgumentNullException">When userId or projectId are null.</exception>
        public bool IsUserProjectManager(ProjectId projectId, UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (projectId == null)
            {
                throw new ArgumentNullException("projectId");
            }

            var managerProjectRole = _userReadRepository.ProjectRoles.First(x => x.Name == managerRoleName);

            var teamMembers = _userReadRepository.ProjectRoleRelations
                .Where(x => x.ProjectId == projectId.Id).ToArray();

            return teamMembers.FirstOrDefault(x => x.UserId == userId.Id && x.ProjectRoleId == managerProjectRole.Id) != null;
        }
    }
}