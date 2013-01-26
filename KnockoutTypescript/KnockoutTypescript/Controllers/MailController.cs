using KnockoutTypescript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnockoutTypescript.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        public ActionResult Index(int folderId)
        {
            var folder = new MailFolder();

            switch (folderId)
            {
                case 1 :
                folder.Mails = new List<Mail> {
                    new Mail { From = "heniek@heniek.pl", To = "henki@henki.pl", Subject = "Życzenia Noworoczne", Date = DateTime.Now },
                    new Mail { From = "benedykt16@vatican.va", To = "eugeniusz@kowalski.com", Subject = "Wyniki kontroli", Date= new DateTime(2012, 12, 12) }
                };
                break;

                default:
                folder.Mails = new List<Mail> {
                    new Mail { From = "eugeniusz@gmail.com", To = "all@kotlownia.pl", Subject = "Wezwanie", Date = DateTime.Now },
                    new Mail { From = "teresa@starachowice.com", To = "eugeniusz@kowalski.com", Subject = "wiadomości", Date= new DateTime(2012, 12, 24) }
                };
                break;
            }
            
            return Json(folder, JsonRequestBehavior.AllowGet);
        }

    }
}
