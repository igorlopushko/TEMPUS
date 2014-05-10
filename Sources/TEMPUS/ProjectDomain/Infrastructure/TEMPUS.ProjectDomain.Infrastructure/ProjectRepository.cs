using System;
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
                throw new ArgumentNullException("projectStorage");

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
                throw new ArgumentNullException("id", "ProjectId must be specified.");

            var project = _projectStorage.Get(id);
            //TODO: Change creating project when Project ctor implemented.
            return project == null ? null : new Project();
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
                PpsClassificationId = root.Classification.Id
            };

            if (root.IsNew)
            {
                _projectStorage.Add(project);
            }
            else if (root.IsDeleted)
            {
                _projectStorage.Delete(project);
            }
            else
            {
                _projectStorage.Update(project);
            }
        }
    }
}