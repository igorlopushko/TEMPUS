using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEMPUS.WebSite.Models.Project
{
    public class ProjectListViewModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Manager { get; set; }
        public String Department { get; set; }
        public String Description { get; set; }
    }
}