using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.DB.Models.Project;
using TEMPUS.ProjectDomain.Services;

namespace TEMPUS.ProjectDomain.Infrastructure
{
    /// <summary>
    /// The class represents functionality for saving, updating, removing data from DB.
    /// </summary>
    public class ProjectStorage : IProjectStorage<DB.Models.Project.Project>
    {
        private readonly DataContext _context;
        private const string OwnerRoleName = "Owner";
        private const string ManagerRoleName = "Manager";

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

            var project = _context.Projects.FirstOrDefault(x => x.Id == id.Id && x.IsDeleted == false);
            if (project != null)
            {
                //TODO: Added getting Tasks, Risks.
                project.TeamMembers = this.GetTeamMembers(project.Id);

                project.ManagerId = this.GetUserIdByProjectRole(id, ManagerRoleName);

                project.OwnerId = this.GetUserIdByProjectRole(id, OwnerRoleName);
            }

            return project;
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

            var projectId = new ProjectId(entity.Id);

            _context.Projects.Add(entity);

            this.ChangeProjectRole(projectId, new UserId(entity.OwnerId), OwnerRoleName, entity);

            this.ChangeProjectRole(projectId, new UserId(entity.ManagerId), ManagerRoleName, entity);

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
            project.IsDeleted = aggregate.IsDeleted;

            var projectId = new ProjectId(aggregate.Id);

            this.ChangeProjectRole(projectId, new UserId(aggregate.OwnerId), OwnerRoleName, project);

            this.ChangeProjectRole(projectId, new UserId(aggregate.ManagerId), ManagerRoleName, project);

            //TODO: Add updating Tasks, Risks.
            foreach (var teamMember in aggregate.TeamMembers)
            {
                _context.ProjectRoleRelations.AddOrUpdate(teamMember);
            }

            _context.SaveChanges();
        }

        private IEnumerable<ProjectRoleRelation> GetTeamMembers(Guid projectId)
        {
            return _context.ProjectRoleRelations.Where(x => x.ProjectId == projectId)
                .ToArray()
                .Select(x => new ProjectRoleRelation
                {
                    UserId = x.UserId,
                    ProjectId = x.ProjectId,
                    ProjectRoleId = x.ProjectRoleId
                });
        }

        private void ChangeProjectRole(ProjectId projectId, UserId managerId, string roleName, DB.Models.Project.Project project)
        {
            var role = _context.ProjectRoles.FirstOrDefault(x => x.Name == roleName);

            if (role != null)
            {
                var oldRole = _context.ProjectRoleRelations.FirstOrDefault(
                    x => x.ProjectId == projectId.Id && x.ProjectRoleId == role.Id);

                if (oldRole == null)
                {
                    _context.ProjectRoleRelations.Add(new ProjectRoleRelation
                        {
                            ProjectId = projectId.Id,
                            ProjectRoleId = role.Id,
                            UserId = managerId.Id,
                            StartDate = project.StartDate,
                            EndDate = project.EndDate
                            //Investigate FTE for role.
                        });
                    return;
                }

                if (oldRole.UserId != managerId.Id)
                {
                    _context.ProjectRoleRelations.Remove(oldRole);

                    _context.ProjectRoleRelations.Add(new ProjectRoleRelation
                        {
                            ProjectId = projectId.Id,
                            ProjectRoleId = role.Id,
                            UserId = managerId.Id,
                            StartDate = project.StartDate,
                            EndDate = project.EndDate
                            //Investigate FTE for role.
                        });
                }
            }
        }

        private Guid GetUserIdByProjectRole(ProjectId id, string roleName)
        {
            var role = _context.ProjectRoles.FirstOrDefault(x => x.Name == roleName);
            if (role != null)
            {
                var manager = _context.ProjectRoleRelations.FirstOrDefault(
                        x => x.ProjectId == id.Id && x.ProjectRoleId == role.Id);

                return manager != null ? manager.UserId : Guid.Empty;
            }

            return Guid.Empty;
        }
    }
}