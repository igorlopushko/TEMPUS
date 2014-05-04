using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Controllers
{
    public class FlowController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}