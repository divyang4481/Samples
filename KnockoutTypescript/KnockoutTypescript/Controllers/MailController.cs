﻿using KnockoutTypescript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnockoutTypescript.Controllers
{
    public class MailController : Controller
    {
        private Repository repository = new Repository();

        [HttpGet]
        public ActionResult Index()
        {
            return Json(repository.Mails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            return Json(repository.Mails.SingleOrDefault(m => m.Id == id), JsonRequestBehavior.AllowGet);
        }

    }
}