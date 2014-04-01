using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Text;

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