using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Manager { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
    }
}