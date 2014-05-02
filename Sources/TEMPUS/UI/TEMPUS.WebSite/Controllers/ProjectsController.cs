using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Controllers
{
    public class ProjectsController : BaseController
    {
        //
        // GET: /Projects/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Plan()
        {
            return View();
        }
    }
}