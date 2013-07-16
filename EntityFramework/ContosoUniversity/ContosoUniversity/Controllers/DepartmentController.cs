using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;

namespace ContosoUniversity.Controllers
{
    public class DepartmentController : Controller
    {
        private IUnitOfWork unitOfWork;
        
        public DepartmentController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var departments = unitOfWork.DepartmentRepository.Get().IncludeProperties<Department>(d => d.Administrator);
            return View(departments.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            var department = unitOfWork.DepartmentRepository.GetById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(unitOfWork.InstructorRepository.Get(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Insert(department);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(unitOfWork.InstructorRepository.Get(), "Id", "FullName", department.PersonId);
            return View(department);
        }

        public ActionResult Edit(int id = 0)
        {
            var department = unitOfWork.DepartmentRepository.GetById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(unitOfWork.InstructorRepository.Get(), "Id", "FullName", department.PersonId);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.DepartmentRepository.Update(department);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Department)entry.GetDatabaseValues().ToObject();
                var clientValues = (Department)entry.Entity;
                
                if (databaseValues.Name != clientValues.Name)
                {
                    ModelState.AddModelError("Name", "Current value: " + databaseValues.Name);
                }

                if (databaseValues.Budget != clientValues.Budget)
                {
                    ModelState.AddModelError("Budget", "Current value: " + string.Format("{0:c}", databaseValues.Budget));
                }

                if (databaseValues.StartDate != clientValues.StartDate)
                {
                    ModelState.AddModelError("StartDate", "Current value: " + string.Format("{0:d}", databaseValues.StartDate));
                }

                if (databaseValues.PersonId != clientValues.PersonId)
                {
                    ModelState.AddModelError("PersonId", "Current value: " + unitOfWork.InstructorRepository.GetById(databaseValues.PersonId).FullName);
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was canceled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");
                department.Timestamp = databaseValues.Timestamp;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after Exception)
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            ViewBag.PersonId = new SelectList(unitOfWork.InstructorRepository.Get(), "Id", "FullName", department.PersonId);
            return View(department);
        }

        public ActionResult Delete(int id, bool? concurrencyError)
        {
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }
        
            var department = unitOfWork.DepartmentRepository.GetById(id);
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Department department)
        {
            try
            {
                unitOfWork.DepartmentRepository.Delete(department);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary { { "concurrencyError", true } });
            }
            catch (DataException)
            {
                //Log the error (add a variable name after Exception)
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                return View(department);
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
