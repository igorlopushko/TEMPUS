using System;
using System.Linq;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality for saving, updating, deleting User.
    /// </summary>
    public class UserRepository : Repository<User, UserId>, IUserRepository
    {
        private readonly IUserStorage<DB.Models.User.User> _userStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="userStorage">The user storage.</param>
        /// <exception cref="System.ArgumentNullException">When userStorage is null.</exception>
        public UserRepository(IEventStore eventStore, IUserStorage<DB.Models.User.User> userStorage)
            : base(eventStore)
        {
            if (userStorage == null)
                throw new ArgumentNullException("userStorage");

            _userStorage = userStorage;
        }

        /// <summary>
        /// Get user aggregate root by identity.
        /// </summary>
        /// <param name="id">user identity</param>
        /// <returns></returns>
        public override User Get(UserId id)
        {
            if (id == null) throw new ArgumentNullException("id");

            var user = _userStorage.Get(id);
            return user == null
                       ? null
                       : new User(
                             new UserId(user.Id),
                             user.FirstName,
                             user.LastName,
                             user.Email,
                             user.Password,
                             user.Image,
                             user.Phone,
                             user.DateOfBirth,
                             user.Roles.ToList(),
                             user.Mood == null ? null : new UserMood(user.Mood.Date, user.Mood.Rate));
        }

        /// <summary>
        /// Save user aggregate root state.
        /// </summary>
        /// <param name="aggregate">The user aggregate.</param>
        public override void Save(User aggregate)
        {
            var user = new DB.Models.User.User
                {
                    Id = aggregate.Id.Id,
                    FirstName = aggregate.FirstName,
                    LastName = aggregate.LastName,
                    Image = aggregate.Image,
                    Password = aggregate.Password,
                    Phone = aggregate.Phone,
                    Email = aggregate.Email,
                    DateOfBirth = aggregate.DateOfBirth,
                    Roles = aggregate.Roles,
                    Mood = aggregate.Mood == null ? null : new DB.Models.User.UserMood
                        {
                            Date = aggregate.Mood.Date,
                            Rate = aggregate.Mood.Rate,
                            UserId = aggregate.Id.Id
                        }
                };

            if (aggregate.IsNew)
            {
                _userStorage.Add(user);
            }
            else if (aggregate.IsDeleted)
            {
                _userStorage.Delete(user);
            }
            else
            {
                _userStorage.Update(user);
            }
        }
    }
}