using System.Collections.Generic;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectViewModel
    {
        public string Name { get; set; }
        public TaskViewModel[] Tasks { get; set; }
    }
}