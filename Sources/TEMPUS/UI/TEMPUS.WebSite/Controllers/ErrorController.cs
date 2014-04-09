using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Default contructor is needed for IoC.
        /// </summary>
        public ErrorController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}