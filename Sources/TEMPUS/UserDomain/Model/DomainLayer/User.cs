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
        private string _firstName;
        private string _lastName;
        private int _age;
        private string _login;
        private string _image;
        private string _password;
        private string _phone;

        private bool _isNew;
        private bool _isDeleted;

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public override UserId Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the user first name.
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
        }

        /// <summary>
        /// Gets the user last name.
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
        }

        /// <summary>
        /// Gets the user age.
        /// </summary>
        public int Age
        {
            get { return _age; }
        }

        /// <summary>
        /// Gets the user login.
        /// </summary>
        public string Login
        {
            get { return _login; }
        }

        /// <summary>
        /// Gets the user password.
        /// </summary>
        public string Password
        {
            get { return _password; }
        }

        /// <summary>
        /// Gets the image which represents user avatar.
        /// </summary>
        public string Image
        {
            get { return _image; }
        }

        /// <summary>
        /// Gets the user phone number.
        /// </summary>
        public string Phone
        {
            get { return _phone; }
        }


        /// <summary>
        /// Gets a value indicating whether aggregate is new.
        /// </summary>
        public bool IsNew
        {
            get { return _isNew; }
        }

        /// <summary>
        /// Gets a value indicating whether aggregate is deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public User(UserId userId)
        {
            _id = userId;
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="login">The user login.</param>
        /// <param name="password">The user password.</param>
        public void CreateUser(string login, string password)
        {
            _login = login;
            _password = password;
            _isNew = true;
            _isDeleted = false;

            var @event = new UserCreated(_id, _login, _password);

            this.ApplyChange(@event);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        public void DeleteUser()
        {
            _isNew = false;
            _isDeleted = true;

            var @event = new UserDeleted(_id);

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
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _age = age;
            _image = image;
            _phone = phone;
            _isNew = true;
            _isDeleted = false;

            var @event = new UserInformationChanged(_id, _age, _phone, _image, _password, _firstName, _lastName);

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