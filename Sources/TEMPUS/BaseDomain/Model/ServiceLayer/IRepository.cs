using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.BaseDomain.Model.ServiceLayer
{
    public interface IRepository<TRoot, TId>
        where TId : IIdentity
        where TRoot : class, IAggregateRoot<TId>
    {
        TRoot Get(TId id);
        void Save(TRoot root);
    }
}