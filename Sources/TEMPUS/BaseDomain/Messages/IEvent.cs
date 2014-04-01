namespace TEMPUS.BaseDomain.Messages
{
    public interface IEvent : IMessage
    { }

    public interface IEvent<out T> : IEvent
        where T : IIdentity
    {
        T Id { get; }
        int Version { get; set; }
    }
}