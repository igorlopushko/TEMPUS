using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.UserDomain.Services.ServiceLayer;

namespace TEMPUS.UserDomain.Infrastructure
{
    public class UserEventHandler : IUserEventHandler
    {
        public void Handle(UserCreated msg)
        {
            throw new NotImplementedException();
        }
    }
}