using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Conference.Models;

namespace Conference.Controllers
{
    public class SessionController : Controller
    {
        private ConferenceContext db = new ConferenceContext();

        [ChildActionOnly]
        public PartialViewResult _GetForSpeaker(int speakerId)
        {
            var sessions = db.Sessions.Where(s => s.SpeakerId == speakerId).ToList();
            return PartialView(sessions);
        }

        public ActionResult Index()
        {
            var sessions = db.Sessions.Include(s => s.Speaker);
            return View(sessions.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        public ActionResult Create()
        {
            ViewBag.SpeakerId = new SelectList(db.Speakers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpeakerId = new SelectList(db.Speakers, "Id", "Name", session.SpeakerId);
            return View(session);
        }

        public ActionResult Edit(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpeakerId = new SelectList(db.Speakers, "Id", "Name", session.SpeakerId);
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeakerId = new SelectList(db.Speakers, "Id", "Name", session.SpeakerId);
            return View(session);
        }

        public ActionResult Delete(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}