using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;
using System.Linq.Expressions;

namespace ContosoUniversity.Controllers
{
    public class CourseController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CourseController()
        {
            unitOfWork = new UnitOfWork(); // TODO: Inject
        }

        public ActionResult Index()
        {
            var courses = unitOfWork.CourseRepository.Get().IncludeProperties<Course>(c => c.Department);
            return View(courses.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            var course = unitOfWork.CourseRepository.GetById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CourseRepository.Insert(course);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            PopulateDepartmentsDropDownList(course.DepartmentId);
            return View(course);
        }

        public ActionResult Edit(int id)
        {
            var course = unitOfWork.CourseRepository.GetById(id);
            PopulateDepartmentsDropDownList(course.DepartmentId);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CourseRepository.Update(course);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentId);
            return View(course);
        }

        public ActionResult Delete(int id = 0)
        {
            var course = unitOfWork.CourseRepository.GetById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = unitOfWork.CourseRepository.GetById(id);
            unitOfWork.CourseRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = unitOfWork.DepartmentRepository.Get(orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DepartmentId = new SelectList(departmentsQuery, "Id", "Name", selectedDepartment);
        }
    }
}
