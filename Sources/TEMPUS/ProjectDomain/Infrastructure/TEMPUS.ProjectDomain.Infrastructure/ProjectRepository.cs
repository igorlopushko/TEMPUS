using System;
using System.Linq;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Services;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    public class ProjectRepository : Repository<Project, ProjectId>, IProjectRepository
    {
        private readonly IProjectStorage<DB.Models.Project.Project> _projectStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository" /> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="projectStorage">The project storage.</param>
        /// <exception cref="System.ArgumentNullException">When projectStorage is null.</exception>
        public ProjectRepository(IEventStore eventStore, IProjectStorage<DB.Models.Project.Project> projectStorage)
            : base(eventStore)
        {
            if (projectStorage == null)
            {
                throw new ArgumentNullException("projectStorage");
            }

            _projectStorage = projectStorage;
        }

        /// <summary>
        /// Gets the specified project aggregate.
        /// </summary>
        /// <param name="id">The project identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">ProjectId must be specified.</exception>
        public override Project Get(ProjectId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id", "ProjectId must be specified.");
            }

            var projectEntity = _projectStorage.Get(id);
            //TODO: Change project ctor when implemented.
            var projectAggregate = projectEntity == null
                                       ? null
                                       : new Project(
                                             new ProjectId(projectEntity.Id),
                                             projectEntity.Name,
                                             projectEntity.Description,
                                             projectEntity.ProjectOrderer,
                                             projectEntity.RecievingOrganization,
                                             projectEntity.Mandatory,
                                             projectEntity.StartDate,
                                             projectEntity.EndDate,
                                             projectEntity.Department == null
                                                 ? null
                                                 : new Department(
                                                       projectEntity.Department.Id, projectEntity.Department.Name),
                                             projectEntity.PpsClassification == null
                                                 ? null
                                                 : new PpsClassification(
                                                       projectEntity.PpsClassification.Id,
                                                       projectEntity.PpsClassification.Name),
                                             null,
                                             null,
                                             new UserId(projectEntity.OwnerId),
                                             new UserId(projectEntity.ManagerId),
                                             projectEntity.TeamMembers.Select(x => new TeamMember
                                                 {
                                                     UserId = new UserId(x.UserId),
                                                     RoleId = x.ProjectRoleId
                                                 }).ToList(),
                                             projectEntity.IsDeleted);

            return projectAggregate;
        }

        /// <summary>
        /// Saves the specified project aggregate root.
        /// </summary>
        /// <param name="root">The project aggregate root.</param>
        public override void Save(Project root)
        {
            var project = new DB.Models.Project.Project
                {
                    Id = root.Id.Id,
                    Mandatory = root.Mandatory,
                    Name = root.Name,
                    Description = root.Description,
                    ProjectOrderer = root.ProjectOrderer,
                    RecievingOrganization = root.RecievingOrganization,
                    StartDate = root.StartDate,
                    EndDate = root.EndDate,
                    DepartmentId = root.Department.Id,
                    PpsClassificationId = root.Classification.Id,
                    IsDeleted = root.IsDeleted,
                    TeamMembers = root.TeamMembers == null
                            ? null
                            : root.TeamMembers.Select(x => new DB.Models.Project.ProjectRoleRelation
                                {
                                    UserId = x.UserId.Id,
                                    ProjectRoleId = x.RoleId,
                                    ProjectId = root.Id.Id
                                }),
                    ManagerId = root.Manager.Id,
                    OwnerId = root.Owner.Id
                };

            if (root.IsNew)
            {
                _projectStorage.Add(project);
            }
            else
            {
                _projectStorage.Update(project);
            }
        }
    }
}