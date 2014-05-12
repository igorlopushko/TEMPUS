using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEMPUS.WebSite.Models.Project;

namespace TEMPUS.WebSite.Models.Task
{
    public class RiskViewModel
    {
        public String Description { get; set; }
        public String Type { get; set; }
        public String Probability { get; set; }
        public String Impact { get; set; }
        public String Response { get; set; }
        public String Status { get; set; }
        public String Person { get; set; }
        public DateTime FinishDate { get; set; }
    }
}