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
        public int Age { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Image { get; private set; }
        public string Phone { get; private set; }

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
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        /// <param name="age">The user age.</param>
        /// <param name="image">The user image.</param>
        /// <param name="phone">The user phone.</param>
        public User(UserId userId, string firstName, string lastName, string login, string password, int age,
            string image, string phone)
        {
            this._id = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Login = login;
            this.Password = password;
            this.Age = age;
            this.Image = image;
            this.Phone = phone;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        public void CreateUser(string login, string password)
        {
            this.Login = login;
            this.Password = password;
            this.IsNew = true;
            this.IsDeleted = false;

            var @event = new UserCreated(this.Id, this.Login, this.Password);

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
        /// <param name="password">The user password.</param>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="age">The user age.</param>
        /// <param name="image">The user image.</param>
        /// <param name="phone">The user phone.</param>
        public void ChangeInformation(string password, string firstName, string lastName, int age, string image,
            string phone)
        {
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Image = image;
            this.Phone = phone;
            this.IsNew = true;
            this.IsDeleted = false;

            var @event = new UserInformationChanged(this.Id, this.Age, this.Phone, this.Image, this.Password,
                this.FirstName, this.LastName);

            this.ApplyChange(@event);
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
    }
}