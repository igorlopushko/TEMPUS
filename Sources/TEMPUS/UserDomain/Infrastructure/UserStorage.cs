using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.DB.Models.User;
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
            var user = _context.Users.Find(id.Id);
            if (user != null)
            {
                user.Roles = this.GetUserRoles(id);
                user.Moods = this.GetUserMoods(id);
            }

            return user;
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

            var user = _context.Users.Find(aggregate.Id);
            user.FirstName = aggregate.FirstName;
            user.LastName = aggregate.LastName;
            user.DateOfBirth = aggregate.DateOfBirth;
            user.Image = aggregate.Image;
            user.Phone = aggregate.Phone;

            foreach (var role in aggregate.Roles)
            {
                _context.UserRoleRelations.AddOrUpdate(new UserRoleRelation { RoleId = role, UserId = aggregate.Id });
            }

            foreach (var mood in aggregate.Moods)
            {
                _context.Moods.AddOrUpdate(new Mood { Date = mood.Key, Rate = mood.Value, UserId = aggregate.Id });
            }

            _context.SaveChanges();
        }

        private IEnumerable<Guid> GetUserRoles(UserId userId)
        {
            return _context.UserRoleRelations.Where(x => x.UserId == userId.Id).AsEnumerable().Select(x => x.RoleId);
        }

        private IEnumerable<KeyValuePair<DateTime, int>> GetUserMoods(UserId userId)
        {
            return _context.Moods.Where(x => x.UserId == userId.Id).ToArray().Select(
                    x => new KeyValuePair<DateTime, int>(x.Date, x.Rate));
        }
    }
}