using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTypeScriptFirst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Using knockout.js library together with TypeScript.";

            return View();
        }

        public ActionResult SimpleBinding()
        {
            return View();
        }

        public ActionResult ComputedValues()
        {
            return View();
        }
    }
}
