using KnockoutTypescript.Models._006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnockoutTypescript.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var tasks = new List<Task>
            {
                new Task { IsDone = false, Title = "Zutylizować odpady" },
                new Task { IsDone = true, Title = "Zlikwidować własność prywatną" },
                new Task { IsDone = false, Title = "Sprywatyzować przemysł włókienniczy" }
            };

            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(IEnumerable<Task> tasks)
        {
            return Json("Success");
        }
    }
}
