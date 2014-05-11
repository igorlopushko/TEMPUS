using System;

namespace TEMPUS.ProjectDomain.Services.DomainLayer
{
    /// <summary>
    /// The class represents service for handling commands related to project.
    /// </summary>
    public class ProjectCommandService : IProjectCommandService
    {
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCommandService"/> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <exception cref="System.ArgumentNullException">When projectRepository is null.</exception>
        public ProjectCommandService(IProjectRepository projectRepository)
        {
            if(projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            _projectRepository = projectRepository;
        }
    }
}