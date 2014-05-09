using System;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.UserDomain.Model.DomainLayer
{
    /// <summary>
    /// The class represents the user.
    /// </summary>
    public class User : AggregateRoot<UserId>
    {
        private readonly UserId _id;

        public override UserId Id { get { return _id; } }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Image { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public IList<Guid> Roles { get; private set; }
        public IList<KeyValuePair<DateTime, int>> Moods { get; private set; }

        public bool IsNew { get; private set; }
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public User(UserId userId)
        {
            this._id = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        /// <param name="image">The user image.</param>
        /// <param name="phone">The user phone.</param>
        /// <param name="dateOfBirth">The date of birth of the user.</param>
        /// <param name="roles">The roles of the user.</param>
        /// <param name="moods">The moods of the user.</param>
        public User(UserId userId, string firstName, string lastName, string login, string password,
            string image, string phone, DateTime dateOfBirth, IList<Guid> roles, IList<KeyValuePair<DateTime, int>> moods)
        {
            this._id = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = login;
            this.Password = password;
            this.Image = image;
            this.Phone = phone;
            this.DateOfBirth = dateOfBirth;
            this.Roles = roles;
            this.Moods = moods;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        public void CreateUser(string login, string password)
        {
            this.Email = login;
            this.Password = password;
            //Done intentionally, because SQL throw error when try to set DateTime.MinValue.
            this.DateOfBirth = DateTime.Now;
            this.IsNew = true;
            this.IsDeleted = false;

            var @event = new UserCreated(this.Id, this.Email, this.Password);

            this.ApplyChange(@event);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        public void DeleteUser()
        {
            this.IsNew = false;
            this.IsDeleted = true;

            var @event = new UserDeleted(this.Id);

            this.ApplyChange(@event);
        }

        /// <summary>
        /// Changes the information.
        /// </summary>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="image">The user image.</param>
        /// <param name="phone">The user phone.</param>
        /// <param name="dateOfBirth">The date of birth of the user.</param>
        public void ChangeInformation(string firstName, string lastName, string image, string phone,
            DateTime dateOfBirth)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Image = image;
            this.Phone = phone;
            this.DateOfBirth = dateOfBirth;
            this.IsNew = false;
            this.IsDeleted = false;

            var @event = new UserInformationChanged(this.Id, this.Phone, this.Image, this.FirstName, this.LastName, this.DateOfBirth);

            this.ApplyChange(@event);
        }

        /// <summary>
        /// Changes the user role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        public void AddRole(Guid roleId)
        {
            this.Roles.Add(roleId);
            this.IsNew = false;
            this.IsDeleted = false;

            var @event = new RoleToUserAdded(this.Id, roleId);

            this.ApplyChange(@event);
        }

        public void AddMood(int rate, DateTime date)
        {
            this.Moods.Add(new KeyValuePair<DateTime, int>(date, rate));
            this.IsNew = false;
            this.IsDeleted = false;

            var @event = new UserMoodSet(this.Id, rate, date);

            this.Apply(@event);
        }

        private void Apply(UserCreated @event)
        {
            //TODO: Investigate why we need this.
        }

        private void Apply(UserInformationChanged @event)
        {
            //TODO: Investigate why we need this.
        }

        private void Apply(UserDeleted @event)
        {
            //TODO: Investigate why we need this.
        }

        private void Apply(RoleToUserAdded @event)
        {
            //TODO: Investigate why we need this.
        }

        private void Apply(UserMoodSet @event)
        {
            //TODO: Investigate why we need this.
        }
    }
}