using System;
using TEMPUS.WebSite.Models.Team;

namespace TEMPUS.WebSite.Models.Project
{
    public class TimeRecordViewModel
    {
        public TaskViewModel Task { get; set; }
        public ProjectViewModel Project { get; set; }
        public UserViewModel User { get; set; }
        public double Hours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}