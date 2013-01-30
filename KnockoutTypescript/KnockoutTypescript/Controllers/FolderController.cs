using KnockoutTypescript.Models._004;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnockoutTypescript.Controllers
{
    // Used in 004 webmail

    public class FolderController : Controller
    {
        private Repository repository = new Repository();

        [HttpGet]
        public ActionResult Index()
        {
            return Json(repository.Folders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            return Json(repository.Folders.SingleOrDefault(f => f.Id == id), JsonRequestBehavior.AllowGet);
        }
    }
}
