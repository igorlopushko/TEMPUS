using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    /// <summary>
    /// The class represents data access operations related to User.
    /// </summary>
    public class UserStorage : IUserStorage<DB.Models.User.User>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStorage"/> class.
        /// </summary>
        /// <param name="context">The user data context.</param>
        /// <exception cref="System.ArgumentNullException">When the context in null.</exception>
        public UserStorage(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
        }

        /// <summary>
        /// Adds the specified user aggregate.
        /// </summary>
        /// <param name="entity">The user aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When user aggregate is null.</exception>
        public void Add(DB.Models.User.User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified user aggregate.
        /// </summary>
        /// <param name="entity">The user aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When user aggregate is null.</exception>
        public void Delete(DB.Models.User.User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the specified user aggregate.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>User aggregate.</returns>
        /// <exception cref="System.ArgumentNullException">When user identifier is null.</exception>
        public DB.Models.User.User Get(UserId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return _context.Users.FirstOrDefault(x => x.Id == id.Id);
        }

        /// <summary>
        /// Gets the specified enumerable of users.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<DB.Models.User.User> Get(Expression<Func<DB.Models.User.User, bool>> expression)
        {
            return _context.Users.Where(expression).AsEnumerable();
        }

        /// <summary>
        /// Updates the specified user aggregate.
        /// </summary>
        /// <param name="aggregate">The user aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When user aggregate is null.</exception>
        public void Update(DB.Models.User.User aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException("aggregate");
            }

            _context.SaveChanges();
        }
    }
}