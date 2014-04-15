using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly IUserReadStorage<UserInfo> _userReadStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserReadRepository"/> class.
        /// </summary>
        /// <param name="userReadStorage">The user read storage.</param>
        /// <exception cref="System.ArgumentNullException">userReadStorage</exception>
        public UserReadRepository(IUserReadStorage<UserInfo> userReadStorage)
        {
            if (userReadStorage == null)
                throw new ArgumentNullException("userReadStorage");

            _userReadStorage = userReadStorage;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">When user id is null.</exception>
        public UserInfo Get(UserId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return _userReadStorage.Get(id);
        }

        /// <summary>
        /// Gets the enumerable of users info.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<UserInfo> Get(Expression<Func<UserInfo, bool>> expression)
        {
            return _userReadStorage.Get(expression);
        }

        /// <summary>
        /// Gets all users info.
        /// </summary>
        public IQueryable<UserInfo> All
        {
            get { return _userReadStorage.All; }
        }
    }
}