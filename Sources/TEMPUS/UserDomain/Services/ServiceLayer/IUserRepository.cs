﻿using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.ServiceLayer;
using TEMPUS.UserDomain.Model.DomainLayer;

namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserRepository : IRepository<User, UserId>
    {
    }
}