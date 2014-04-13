using System;
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

        private bool _isNew;

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

        public bool IsNew
        {
            get { return _isNew; }
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
            _isNew = true;

            var @event = new UserCreated(_id);

            ApplyChange(@event);
        }

        private void Apply(UserCreated @event)
        {
            
        }
    }
}