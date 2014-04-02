namespace TEMPUS.BaseDomain.Messages
{
    public interface IHandle<T> where T : IMessage
    {
        void Handle(T msg);
    }
}