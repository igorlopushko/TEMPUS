using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.DomainLayer;
using TEMPUS.UserDomain.Services;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserRepository : Repository<User, UserId>, IUserRepository
    {
        private IUserQueryService _userQueryService;

        public UserRepository(IEventStore eventStore, IUserQueryService userQueryService)
            : base(eventStore)
        {
            _userQueryService = userQueryService;
        }

        public override User Get(UserId id)
        {
            var user = _userQueryService.GetUserById(id);

            return new User(id, user.FirstName, user.LastName, user.Login, user.Password, user.Age, user.Image,
                user.Phone);
        }

        public override void Save(User root)
        {
            throw new System.NotImplementedException();
        }
    }
}