using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : Controller
    {
        private IUnitOfWork unitOfWork;
        
        public InstructorController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public ActionResult Index(int? id, int? courseId)
        {
            var viewModel = new InstructorIndexData
                {
                    Instructors = unitOfWork.InstructorRepository
                        .IncludeProperties(
                            i => i.OfficeAssignment,
                            i => i.Courses.Select(c => c.Department))
                        .Get(orderBy: i => i.OrderBy(x => x.LastName))
                };

            if (id != null)
            {
                ViewBag.PersonId = id.Value;
                viewModel.Courses = viewModel.Instructors.Single(i => i.Id == id.Value).Courses;
            }

            if (courseId != null)
            {
                ViewBag.CourseId = courseId.Value;

                var selectedCourse = viewModel.Courses.Single(x => x.Id == courseId);
                //db.Entry(selectedCourse).Collection(x => x.Enrollments).Load(); // explicit loading
                
                //foreach (Enrollment enrollment in selectedCourse.Enrollments)
                //{
                //    db.Entry(enrollment).Reference(x => x.Student).Load(); // explicit loading
                //}

                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        public ActionResult Details(int id = 0)
        {
            var instructor = unitOfWork.InstructorRepository.GetById(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(unitOfWork.OfficeAssignmentRepository.Get(), "PersonId", "Location");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.InstructorRepository.Insert(instructor);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(unitOfWork.OfficeAssignmentRepository.Get(), "PersonId", "Location", instructor.Id);
            return View(instructor);
        }

        public ActionResult Edit(int id = 0)
        {
            var instructor = unitOfWork.InstructorRepository
                .Get()
                .Include(i => i.Courses)
               .Single(i => i.Id == id);
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = unitOfWork.CourseRepository.Get();
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.Id));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.Id)
                });
            }
            ViewBag.Courses = viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedCourses)
        {
            var instructorToUpdate = unitOfWork.InstructorRepository
                .IncludeProperties(
                    i => i.OfficeAssignment,
                    i => i.Courses)
                .Get()
                .Single(i => i.Id == id);

            if (TryUpdateModel(instructorToUpdate, "", null, new[] { "Courses" } /* Exclude properties */))
            {
                try
                {
                    // Handle OfficeAssignment update
                    if (string.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                    unitOfWork.InstructorRepository.Update(instructorToUpdate);
                    unitOfWork.Save();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    return View();
                }
            }

            PopulateAssignedCourseData(instructorToUpdate);

            return View(instructorToUpdate);
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null) // No assigned courses
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.Id));

            foreach (var course in unitOfWork.CourseRepository.Get())
            {
                if (selectedCoursesHS.Contains(course.Id.ToString()))
                {
                    if (!instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.Id))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        public ActionResult Delete(int id = 0)
        {
            var instructor = unitOfWork.InstructorRepository.GetById(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.InstructorRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
