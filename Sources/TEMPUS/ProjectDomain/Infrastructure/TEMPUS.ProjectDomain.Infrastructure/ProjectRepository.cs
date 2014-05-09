using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMPUS.BaseDomain.Infrastructure;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Services;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    public class ProjectRepository : Repository<Project, ProjectId>, IProjectRepository
    {
        private readonly IProjectStorage<DB.Models.Project.Project> _projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="projectRepository" /> class.
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="projectRepository">The project storage.</param>
        /// <exception cref="System.ArgumentNullException">When projectRepository is null.</exception>
         public ProjectRepository(IEventStore eventStore, IProjectStorage<DB.Models.Project.Project> projectRepository)
            : base(eventStore)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            _projectRepository = projectRepository;
        }

         public override Project Get(ProjectId id)
         {
             throw new NotImplementedException();
         }
    }
}