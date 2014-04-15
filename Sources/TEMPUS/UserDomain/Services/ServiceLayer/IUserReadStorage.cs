using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserReadStorage<T> : IReadStorage<T> where T : Dto
    {
    }
}