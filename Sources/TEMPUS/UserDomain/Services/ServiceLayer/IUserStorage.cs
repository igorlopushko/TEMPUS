using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.DB.Models;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserStorage<T> : IStorage<T, UserId>
        where T : Entity
    {
    }
}