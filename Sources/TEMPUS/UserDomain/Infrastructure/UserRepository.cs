using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        private readonly IUserStorage<User, UserId> _userStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="userStorage">The user storage.</param>
        /// <exception cref="System.ArgumentNullException">When userStorage is null.</exception>
        public UserRepository(IEventStore eventStore, IUserStorage<User, UserId> userStorage)
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
            if (id == null)
                throw new ArgumentNullException("id");

            return _userStorage.Get(id);
        }

        /// <summary>
        /// Gets the specified enumerable of users.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<User> Get(Expression<Func<User,bool>> expression)
        {
            return _userStorage.Get(expression);
        }

        /// <summary>
        /// Save user aggregate root state.
        /// </summary>
        /// <param name="aggregate">The user aggregate.</param>
        public override void Save(User aggregate)
        {
            if (aggregate.IsNew)
                _userStorage.Add(aggregate);
            else if (aggregate.IsDeleted)
                _userStorage.Delete(aggregate);
            else
                _userStorage.Update(aggregate);
        }
    }
}