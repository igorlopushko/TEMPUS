using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents data access operations related to User.
    /// </summary>
    public class UserStorage : IUserStorage<User, UserId>
    {
        private readonly UserDataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStorage"/> class.
        /// </summary>
        /// <param name="context">The user data context.</param>
        /// <exception cref="System.ArgumentNullException">When the context in null.</exception>
        public UserStorage(UserDataContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this._context = context;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        public IQueryable<User> All
        {
            get { return _context.Users.AsQueryable(); }
        }

        /// <summary>
        /// Adds the specified user aggregate.
        /// </summary>
        /// <param name="aggregate">The user aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When user aggregate is null.</exception>
        public void Add(User aggregate)
        {
            if(aggregate == null)
                throw new ArgumentNullException("aggregate");
            
            //TODO: Investigate where we need to processing UserInfo.
            _context.Users.Add(aggregate);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified user aggregate.
        /// </summary>
        /// <param name="aggregate">The user aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When user aggregate is null.</exception>
        public void Delete(User aggregate)
        {
            if(aggregate == null)
                throw new ArgumentNullException("aggregate");

            //TODO: Investigate where we need to processing UserInfo.
            _context.Users.Remove(aggregate);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the specified user aggregate.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>User aggregate.</returns>
        /// <exception cref="System.ArgumentNullException">When user identifier is null.</exception>
        public User Get(UserId id)
        {
            if(id == null)
                throw new ArgumentNullException("id");

            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the specified enumerable of users.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<User> Get(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Where(expression).AsEnumerable();
        }

        /// <summary>
        /// Updates the specified user aggregate.
        /// </summary>
        /// <param name="aggregate">The user aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When user aggregate is null.</exception>
        public void Update(User aggregate)
        {
            if(aggregate == null)
                throw new ArgumentNullException("aggregate");

            //TODO: Investigate where we need to processing UserInfo.
            _context.SaveChanges();
        }
    }
}