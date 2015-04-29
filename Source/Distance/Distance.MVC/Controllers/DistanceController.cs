using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Distance.MVC.Controllers
{
    public class DistanceController : Controller
    {
        // GET: Distance
        public ActionResult Distance()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }
    }
}