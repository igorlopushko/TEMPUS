namespace TEMPUS.BaseDomain.Messages
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}