using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEMPUS.WebSite.Models.Project;

namespace TEMPUS.WebSite.Models.Task
{
    public class TaskListViewModel
    {
        public String Name { get; set; }
        public String Person { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}