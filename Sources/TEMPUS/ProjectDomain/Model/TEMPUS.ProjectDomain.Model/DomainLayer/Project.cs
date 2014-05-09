using System;
using System.Collections.Generic;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class Project : AggregateRoot<ProjectId>
    {
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

        public override ProjectId Id
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}