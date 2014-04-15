using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents only read operations related to UserInfo.
    /// </summary>
    public class UserReadStorage : IUserReadStorage<UserInfo>
    {
        private readonly UserDataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserReadStorage{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">When context is null.</exception>
        public UserReadStorage(UserDataContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Gets the specified user info.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <exception cref="System.ArgumentNullException">When id is null.</exception>
        public UserInfo Get(UserId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            return _context.UsersInfo.FirstOrDefault(x => x.UserId == id);
        }

        /// <summary>
        /// Gets all users info.
        /// </summary>
        public IQueryable<UserInfo> All
        {
            get { return _context.UsersInfo.AsQueryable(); }
        }

        /// <summary>
        /// Gets the specified enumerable of users info.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<UserInfo> Get(Expression<Func<UserInfo, bool>> expression)
        {
            return _context.UsersInfo.Where(expression).AsEnumerable();
        }
    }
}