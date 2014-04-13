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
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public User(UserId userId)
        {
            _id = userId;
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
            _id = userId;
            _firstName = firstName;
            _lastName = lastName;
            _login = login;
            _password = password;
            _age = age;
            _image = image;
            _phone = phone;
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

            var @event = new UserCreated(_id, _login, _password);

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

            var @event = new UserInformationChanged(_id, _age, _phone, _image, _password, _firstName, _lastName);

            this.ApplyChange(@event);
        }
    }
}