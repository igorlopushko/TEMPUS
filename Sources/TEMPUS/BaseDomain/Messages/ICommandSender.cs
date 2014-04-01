using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEMPUS.BaseDomain.Messages
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : ICommand;
    }
}