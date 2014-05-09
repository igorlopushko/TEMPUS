using System;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.UserDomain.Model.ServiceLayer;
﻿
namespace TEMPUS.UserDomain.Services.ServiceLayer
{
    public interface IUserQueryService
    {
        UserInfo GetUserByEmail(string emails);
        bool ValidateUser(string login, string password);
        IEnumerable<KeyValuePair<Guid, string>> GetUsersRoles();
        IEnumerable<UserMood> GetTeamMoods(ProjectId projectId);
        IEnumerable<KeyValuePair<DateTime, int>> GetUserMoods(UserId userId);
    }
}