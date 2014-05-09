using TEMPUS.BaseDomain.Messages;
using TEMPUS.BaseDomain.Messages.Identities;
using TEMPUS.BaseDomain.Model.DomainLayer;

namespace TEMPUS.ProjectDomain.Model.DomainLayer
{
    public class Project : AggregateRoot<ProjectId>
    {
        public override ProjectId Id
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}