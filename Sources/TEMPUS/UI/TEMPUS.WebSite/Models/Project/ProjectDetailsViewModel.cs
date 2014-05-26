using System;
using TEMPUS.UserDomain.Model.ServiceLayer;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectDetailsViewModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public ProjectStatus Status { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Manager { get; set; }
        public string Department { get; private set; }
        public string Description { get; private set; }

        public ProjectDetailsViewModel(string id,
                                    string name,
                                    ProjectStatus status,
                                    DateTime startDate,
                                    DateTime endDate,
                                    UserInfo manager,
                                    string department,
                                    string description)
        {
            Id = id;
            Name = name;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            Manager = String.Format("{0} {1}", manager.FirstName, manager.LastName);
            Department = department;
            Description = description;
        }
    }
}