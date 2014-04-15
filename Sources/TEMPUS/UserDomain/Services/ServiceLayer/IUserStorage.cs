namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    using TEMPUS.BaseDomain.Messages;
    using TEMPUS.BaseDomain.Model.DomainLayer;
    using TEMPUS.BaseDomain.Model.ServiceLayer;

    public interface IUserStorage<T, TId> : IStorage<T, TId>
        where T : AggregateRoot<TId>
        where TId : GuidIdentity
    {
    }
}