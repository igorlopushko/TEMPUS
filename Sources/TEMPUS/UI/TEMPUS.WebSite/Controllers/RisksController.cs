﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEMPUS.WebSite.Controllers
{
    public class RisksController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
