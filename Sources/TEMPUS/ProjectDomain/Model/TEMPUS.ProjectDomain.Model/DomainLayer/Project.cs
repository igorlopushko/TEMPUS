using System;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class Project : AggregateRoot<ProjectId>
    {
        private readonly ProjectId _id;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ProjectOrderer { get; private set; }
        public string RecievingOrganization { get; private set; }
        public bool Mandatory { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Department Department { get; private set; }
        public PpsClassification Classification { get; private set; }
        public IEnumerable<Task> Tasks { get; private set; }
        public IEnumerable<Risk> Risks { get; private set; }

        public UserId Owner { get; private set; }
        public UserId Manager { get; private set; }
        public IEnumerable<UserId> TeamMembers { get; private set; }

        public bool IsNew { get; private set; }
        public bool IsDeleted { get; private set; }

        public override ProjectId Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="id">The project identifier.</param>
        public Project(ProjectId id)
        {
            _id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="projectOrdered">The project ordered of the project.</param>
        /// <param name="receivingOrganization">The receiving organization of the project.</param>
        /// <param name="mandatory">if set to <c>true</c> project is mandatory.</param>
        /// <param name="startDate">The start date of the project.</param>
        /// <param name="endDate">The end date of the project.</param>
        /// <param name="department">The department of the project.</param>
        /// <param name="classification">The classification of the project.</param>
        /// <param name="tasks">The tasks of the project.</param>
        /// <param name="risks">The risks of the project.</param>
        /// <param name="owner">The owner of the project.</param>
        /// <param name="manager">The manager of the project.</param>
        /// <param name="teamMember">The team members of the project.</param>
        public Project(string name, string description, string projectOrdered, string receivingOrganization, bool mandatory,
            DateTime startDate, DateTime endDate, Department department, PpsClassification classification, IEnumerable<Task> tasks,
            IEnumerable<Risk> risks, UserId owner, UserId manager, IEnumerable<UserId> teamMember)
        {
            this.Name = name;
            this.Description = description;
            this.ProjectOrderer = projectOrdered;
            this.RecievingOrganization = receivingOrganization;
            this.Mandatory = mandatory;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Department = department;
            this.Classification = classification;
            this.Tasks = tasks;
            this.Risks = risks;
            this.Owner = owner;
            this.Manager = manager;
            this.TeamMembers = teamMember;
        }

        /// <summary>
        /// Creates the project.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="projectOrdered">The project ordered.</param>
        /// <param name="receivingOrganization">The receiving organization of the project.</param>
        /// <param name="mandatory">if set to <c>true</c> project is mandatory.</param>
        /// <param name="startDate">The start date of the project.</param>
        /// <param name="endDate">The end date of the project.</param>
        /// <param name="department">The department of the project.</param>
        /// <param name="classification">The classification of the project.</param>
        /// <param name="owner">The owner of the project.</param>
        /// <param name="manager">The manager of the project.</param>
        public void CreateProject(string name, string description, string projectOrdered, string receivingOrganization, bool mandatory,
            DateTime startDate, DateTime endDate, Department department, PpsClassification classification, UserId owner, UserId manager)
        {
            this.Name = name;
            this.Description = description;
            this.ProjectOrderer = projectOrdered;
            this.RecievingOrganization = receivingOrganization;
            this.Mandatory = mandatory;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Department = department;
            this.Classification = classification;
            this.Owner = owner;
            this.Manager = manager;
            this.IsNew = true;
            this.IsDeleted = false;

            var @event = new ProjectCreated(this.Id);

            this.ApplyChange(@event);
        }

        /// <summary>
        /// Changes the information for the project.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        /// <param name="description">The description of the project.</param>
        /// <param name="projectOrdered">The project ordered.</param>
        /// <param name="receivingOrganization">The receiving organization of the project.</param>
        /// <param name="mandatory">if set to <c>true</c> project is mandatory.</param>
        /// <param name="startDate">The start date of the project.</param>
        /// <param name="endDate">The end date of the project.</param>
        /// <param name="department">The department of the project.</param>
        /// <param name="classification">The classification of the project.</param>
        /// <param name="owner">The owner of the project.</param>
        /// <param name="manager">The manager of the project.</param>
        public void ChangeInformation(string name, string description, string projectOrdered, string receivingOrganization, bool mandatory,
            DateTime startDate, DateTime endDate, Department department, PpsClassification classification, UserId owner, UserId manager)
        {
            this.Name = name;
            this.Description = description;
            this.ProjectOrderer = projectOrdered;
            this.RecievingOrganization = receivingOrganization;
            this.Mandatory = mandatory;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Department = department;
            this.Classification = classification;
            this.Owner = owner;
            this.Manager = manager;
            this.IsNew = false;
            this.IsDeleted = false;

            var @event = new ProjectInformationChanged(this.Id, this.Name, this.Description, this.ProjectOrderer,
                this.RecievingOrganization, this.Mandatory, this.StartDate, this.EndDate, this.Department.Id,
                this.Classification.Id, this.Owner, this.Manager);

            this.ApplyChange(@event);
        }

        private void Apply(ProjectCreated @event)
        {
            
        }

        private void Apply(ProjectInformationChanged @event)
        {

        }
    }
}