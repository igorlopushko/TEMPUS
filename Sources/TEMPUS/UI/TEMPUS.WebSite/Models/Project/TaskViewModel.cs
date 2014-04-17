using System;
using TEMPUS.WebSite.Models.Account;

namespace TEMPUS.WebSite.Models.Project
{
    public class TaskViewModel
    {
        public string Name { get; set; }
        public string Asignee { get; set; }  // TODO: should be nested model of user
        public DateTime Created { get; set; }
        public DateTime Due { get; set; }
        public TaskStatus Status { get; set; }
    }
}