using System;
using System.Collections.Generic;
using System.Linq;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.ProjectDomain.Model.DomainLayer;
using TEMPUS.ProjectDomain.Model.ServiceLayer;
using TEMPUS.ProjectDomain.Services;
    
namespace TEMPUS.ProjectDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality for saving, updating, removing data from DB.
    /// </summary>
    public class ProjectQueryService : IProjectQueryService
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectQueryService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">When context is null.</exception>
        public ProjectQueryService(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the user projects.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public IEnumerable<ProjectInfo> GetUserProjects(UserId userId)
        {
            var userInProject = _context.ProjectRoleRelations.Where(x => x.UserId == userId.Id).Select(x => x.ProjectId);

            return _context.Projects.Where(x => userInProject.Contains(x.Id))
                    .AsEnumerable()
                    .Select(x => new ProjectInfo(x.Id, x.Name));
        }
        
        /// <summary>
        /// Gets the project by identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        public ProjectInfo GetProjectById(Guid projectId)
        {
            var project = _context.Projects.FirstOrDefault(x => x.Id == projectId);
            return new ProjectInfo(project.Id, project.Name);
        }

        /// <summary>
        /// Gets the PPS classifications.
        /// </summary>
        public IEnumerable<PpsClassification> GetPpsClassifications()
        {
            return _context.PpsClassifications.AsEnumerable().Select(x => new PpsClassification(x.Id, x.Name));
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.AsEnumerable().Select(x => new Department(x.Id, x.Name));
        }
    }
}