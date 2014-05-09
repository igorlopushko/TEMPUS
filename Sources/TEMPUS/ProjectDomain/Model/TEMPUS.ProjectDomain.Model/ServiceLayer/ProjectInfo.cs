using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMPUS.ProjectDomain.Model.ServiceLayer
{
    /// <summary>
    /// Represent limited project info object.
    /// </summary>
    public class ProjectInfo
    {
        public string Name { get; private set; }

        public ProjectInfo(string name)
        {
            Name = name;
        }
    }
}