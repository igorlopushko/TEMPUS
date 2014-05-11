using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.ProjectDomain.Services;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality for saving, updating, removing data from DB.
    /// </summary>
    public class ProjectStorage : IProjectStorage<DB.Models.Project.Project>
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectStorage"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">When context is null.</exception>
        public ProjectStorage(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the specified project.
        /// </summary>
        /// <param name="id">The project identifier.</param>
        /// <exception cref="System.ArgumentNullException">When id is null</exception>
        public DB.Models.Project.Project Get(ProjectId id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            //TODO: Added getting Tasks, Risks, TeamMembers.
            return _context.Projects.Find(id.Id);
        }

        /// <summary>
        /// Gets the specified enumerable of projects.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public IEnumerable<DB.Models.Project.Project> Get(Expression<Func<DB.Models.Project.Project, bool>> expression)
        {
            return _context.Projects.Where(expression).AsEnumerable();
        }

        /// <summary>
        /// Adds the specified project entity.
        /// </summary>
        /// <param name="entity">The project entity.</param>
        /// <exception cref="System.ArgumentNullException">When entity is null.</exception>
        public void Add(DB.Models.Project.Project entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Projects.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified project entity.
        /// </summary>
        /// <param name="entity">The project entity.</param>
        /// <exception cref="System.ArgumentNullException">When entity is null.</exception>
        public void Delete(DB.Models.Project.Project entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Projects.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified project aggregate.
        /// </summary>
        /// <param name="aggregate">The project aggregate.</param>
        /// <exception cref="System.ArgumentNullException">When aggregate is null.</exception>
        public void Update(DB.Models.Project.Project aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException("aggregate");
            }

            var project = _context.Projects.Find(aggregate.Id);
            project.Name = aggregate.Name;
            project.Description = aggregate.Description;
            project.Mandatory = aggregate.Mandatory;
            project.PpsClassificationId = aggregate.PpsClassificationId;
            project.ProjectOrderer = aggregate.ProjectOrderer;
            project.RecievingOrganization = aggregate.RecievingOrganization;
            project.StartDate = aggregate.StartDate;
            project.EndDate = aggregate.EndDate;
            project.DepartmentId = aggregate.DepartmentId;

            //TODO: Add updating Tasks, Risks, TeamMebers.

            _context.SaveChanges();
        }
    }
}