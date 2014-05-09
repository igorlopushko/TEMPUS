using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class PpsClassification
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public PpsClassification(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}