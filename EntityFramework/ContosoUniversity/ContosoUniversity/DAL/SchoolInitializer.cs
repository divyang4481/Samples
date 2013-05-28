using System;
using System.Collections.Generic;
using System.Data.Entity;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class SchoolInitializer : DropCreateDatabaseAlways<SchoolContext> // DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstMidName = "Carson",   LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") },
                new Student { FirstMidName = "Meredith", LastName = "Alonso",    EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",     EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstMidName = "Yan",      LastName = "Li",        EnrollmentDate = DateTime.Parse("2002-09-01") },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",   EnrollmentDate = DateTime.Parse("2001-09-01") },
                new Student { FirstMidName = "Laura",    LastName = "Norman",    EnrollmentDate = DateTime.Parse("2003-09-01") },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",  EnrollmentDate = DateTime.Parse("2005-09-01") }
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course { Title = "Chemistry",      Credits = 3, },
                new Course { Title = "Microeconomics", Credits = 3, },
                new Course { Title = "Macroeconomics", Credits = 3, },
                new Course { Title = "Calculus",       Credits = 4, },
                new Course { Title = "Trigonometry",   Credits = 4, },
                new Course { Title = "Composition",    Credits = 3, },
                new Course { Title = "Literature",     Credits = 4, }
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, CourseId = 1, Grade = 1 },
                new Enrollment { StudentId = 1, CourseId = 2, Grade = 3 },
                new Enrollment { StudentId = 1, CourseId = 3, Grade = 1 },
                new Enrollment { StudentId = 2, CourseId = 4, Grade = 2 },
                new Enrollment { StudentId = 2, CourseId = 5, Grade = 4 },
                new Enrollment { StudentId = 2, CourseId = 6, Grade = 4 },
                new Enrollment { StudentId = 3, CourseId = 1            },
                new Enrollment { StudentId = 4, CourseId = 1,           },
                new Enrollment { StudentId = 4, CourseId = 2, Grade = 4 },
                new Enrollment { StudentId = 5, CourseId = 3, Grade = 3 },
                new Enrollment { StudentId = 6, CourseId = 4            },
                new Enrollment { StudentId = 7, CourseId = 5, Grade = 2 },
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}