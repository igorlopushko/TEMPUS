namespace TEMPUS.BaseDomain.Messages
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;
    }
}