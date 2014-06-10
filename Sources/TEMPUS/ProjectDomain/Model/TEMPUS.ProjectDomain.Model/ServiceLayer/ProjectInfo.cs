using System;
using TEMPUS.BaseDomain.Model.ServiceLayer;

namespace TEMPUS.ProjectDomain.Model.ServiceLayer
{
    /// <summary>
    /// Represent limited project info object.
    /// </summary>
    [Serializable]
    public class ProjectInfo : Dto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public ProjectInfo(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}