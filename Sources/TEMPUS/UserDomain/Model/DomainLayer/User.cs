using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.UserDomain.Model.DomainLayer
{
    public class User : AggregateRoot<UserId>
    {
        private readonly UserId _id;
        private string _firstName;
        private string _lastName;

        public override UserId Id
        {
            get { return _id; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public User(UserId userId)
        {
             _id = userId;
        }

        public User(UserId userId,
                    string firstName,
                    string lastName)
        {
            _id = userId;
            _firstName = firstName;
            _lastName = lastName;
        }

        public void CreateUser(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;

            var @event = new UserCreated(_id);

            ApplyChange(@event);
        }
    }
}