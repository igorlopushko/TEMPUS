using TEMPUS.DB.Models;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserStorage<T> : IStorage<T>
        where T : Entity
    {
    }
}