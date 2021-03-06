﻿using System;
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
        IEnumerable<UserInfo> GetUsersByProjectId(ProjectId projectId);
        ProjectRole GetProjectRoleForUser(ProjectId projectId, UserId userId);
        UserMood GetUserMood(UserId userId);
        UserInfo GetUser(UserId id);
        IEnumerable<UserInfo> GetUsers();
        IEnumerable<UserInfo> GetAllActiveUsers();
        Guid GetUserRoleId(UserRole role);
        IEnumerable<UserActivity> GetUserActivities(UserId id);
        IEnumerable<UserMainInfo> GetTeamForProjectManager(UserId id, ProjectId projectId);
        bool IsUserProjectManager(ProjectId projectId, UserId userId);
    }
}