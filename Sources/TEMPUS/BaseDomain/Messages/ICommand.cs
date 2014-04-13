namespace TEMPUS.BaseDomain.Messages
{
    public interface ICommand : IMessage
    { }

    public interface ICommand<out T> : ICommand
        where T : IIdentity
    {
        T Id { get; }
    }

    public interface IFunctionalCommand : ICommand
    {
    }
}