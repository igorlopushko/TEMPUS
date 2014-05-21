using System;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.DB;
using TEMPUS.ProjectDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Services.DomainLayer
{
    /// <summary>
    /// The class represents service for handling commands related to project.
    /// </summary>
    public class ProjectCommandService : IProjectCommandService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCommandService" /> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">When projectRepository or context are null.</exception>
        public ProjectCommandService(IProjectRepository projectRepository, DataContext context)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            if (context == null)
                throw new ArgumentNullException("context");

            _projectRepository = projectRepository;
            _context = context;
        }

        /// <summary>
        /// Handles the specified <see cref="CreateProject"/> command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When projectId is null.</exception>
        public void Handle(CreateProject command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("command", "ProjectId is null");
            }

            var project = this.GetOrCreateProject(command.Id);
            var department = this.GetDepartment(command.DepartmentId);
            var classification = this.GetPpsClassification(command.PpsClassificationId);

            if (department == null)
                throw new ArgumentNullException("department");

            if (classification == null)
                throw new ArgumentNullException("classification");

            project.CreateProject(command.Name, command.Description, command.ProjectOrderer,
                command.RecievingOrganization, command.Mandatory,
                command.StartDate, command.EndDate, department, classification, command.OwnerId, command.Manager);

            _projectRepository.Save(project);

        }

        /// <summary>
        /// Handles the specified <see cref="ChangeProjectInformation"/> command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">When projectId is null.</exception>
        public void Handle(ChangeProjectInformation command)
        {
            if (command.Id == null)
            {
                throw new ArgumentNullException("command", "ProjectId is null");
            }

            var project = _projectRepository.Get(command.Id);
            var department = this.GetDepartment(command.DepartmentId);
            var classification = this.GetPpsClassification(command.PpsClassificationId);

            if (department == null)
                throw new ArgumentNullException("department");

            if (classification == null)
                throw new ArgumentNullException("classification");

            project.ChangeInformation(command.Name, command.Description, command.ProjectOrderer,
                command.RecievingOrganization, command.Mandatory,
                command.StartDate, command.EndDate, department, classification, command.OwnerId, command.Manager);

            _projectRepository.Save(project);
        }

        /// <summary>
        /// Handles the specified <see cref="AssignUserToProject"/> command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Handle(AssignUserToProject command)
        {
            if (command.Id == null)
                throw new ArgumentNullException("ProjectId");

            if (command.UserId == null)
                throw new ArgumentNullException("UserId");

            var user = _context.Users.Find(command.UserId.Id);
            if (user == null)
                throw new ArgumentException(string.Format("User with id {0} does not exist.", command.UserId.Id));

            var role = _context.ProjectRoles.Find(command.RoleId);
            if (role == null)
                throw new ArgumentException(string.Format("Project role with id {0} does not exist.", command.UserId.Id));

            var project = _projectRepository.Get(command.Id);
            project.AssignUser(command.UserId, command.RoleId, command.FTE);

            _projectRepository.Save(project);
        }

        private Project GetOrCreateProject(ProjectId id)
        {
            var project = _projectRepository.Get(id);
            if (project != null)
                throw new ArgumentException(string.Format("Project with such id: {0} exist in the system.", id.Id));

            return new Project(id);
        }

        private Department GetDepartment(Guid departmentId)
        {
            var department = _context.Departments.Find(departmentId);
            return department == null ? null : new Department(department.Id, department.Name);
        }

        private PpsClassification GetPpsClassification(Guid ppsClassificationId)
        {
            var classification = _context.PpsClassifications.Find(ppsClassificationId);
            return classification == null ? null : new PpsClassification(classification.Id, classification.Name);
        }
    }
}