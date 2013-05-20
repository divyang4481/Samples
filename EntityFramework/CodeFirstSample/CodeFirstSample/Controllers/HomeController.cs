using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstSample.Models;

namespace CodeFirstSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Blog blog;

            using (var db = new BloggingContext())
            {
                // Create and save a new Blog
                db.Blogs.Add(new Blog { Name = "Blog1" });
                db.SaveChanges();

                // Display all Blogs from the database
                blog = db.Blogs.First(b => b.Name == "Blog1");
            }

            return View(blog);
        }

    }
}
