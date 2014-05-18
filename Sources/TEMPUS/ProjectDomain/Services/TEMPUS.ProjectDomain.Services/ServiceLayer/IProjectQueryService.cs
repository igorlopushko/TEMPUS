using System;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Model.ServiceLayer;

namespace TEMPUS.ProjectDomain.Services
{
    public interface IProjectQueryService
    {
        IEnumerable<ProjectInfo> GetUserProjects(UserId userId);
        ProjectInfo GetProjectById(Guid projectId);
        IEnumerable<PpsClassification> GetPpsClassifications();
        IEnumerable<Department> GetDepartments();
    }
}