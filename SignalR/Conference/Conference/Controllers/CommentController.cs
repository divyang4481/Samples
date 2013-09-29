using Conference.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Conference.Controllers
{
    public class CommentController : Controller
    {
        private ConferenceContext db = new ConferenceContext();

        public PartialViewResult _GetForSession(int sessionId)
        {
            ViewBag.SessionId = sessionId;
            var comments = db.Comments.Where(c => c.SessionId == sessionId).ToList();
            return PartialView("_GetForSession", comments);
        }

        [ChildActionOnly]
        public PartialViewResult _CommentForm(int sessionId)
        {
            var comment = new Comment {SessionId = sessionId};
            return PartialView("_CommentForm", comment);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _Submit(Comment comment)
        {
            // No exception handling
            // No validation

            db.Comments.Add(comment);
            db.SaveChanges();

            var comments = db.Comments.Where(c => c.SessionId == comment.SessionId).ToList();
            ViewBag.SessionId = comment.SessionId;

            return PartialView("_GetForSession", comments);
        }
    }
}
