using System;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class Department
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Department(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}