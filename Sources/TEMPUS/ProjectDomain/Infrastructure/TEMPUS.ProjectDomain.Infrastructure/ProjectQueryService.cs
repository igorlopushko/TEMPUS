using System;
using System.Collections.Generic;
using System.Linq;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
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
    }
}