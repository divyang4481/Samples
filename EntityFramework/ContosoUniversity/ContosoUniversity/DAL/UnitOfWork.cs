using System;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private SchoolContext context = new SchoolContext();
        private GenericRepository<Department> departmentRepository;
        private GenericRepository<Course> courseRepository;
        private GenericRepository<Student> studentRepository;
        private GenericRepository<Instructor> instructorRepository;
        private GenericRepository<OfficeAssignment> officeAssignmentRepository;

        private GenericRepository<T> GetRepositoryLazy<T>(GenericRepository<T> repository)
            where T : class
        {
            if(repository == null)
            {
                repository = new GenericRepository<T>(context);
            }
            
            return repository;
        }

        public GenericRepository<Department> DepartmentRepository
        {
            get { return GetRepositoryLazy<Department>(this.departmentRepository); }
        }

        public GenericRepository<Course> CourseRepository
        {
            get { return GetRepositoryLazy(this.courseRepository); }
        }

        public GenericRepository<Student> StudentRepository
        {
            get { return GetRepositoryLazy(this.studentRepository); }
        }
        
        public GenericRepository<Instructor> InstructorRepository
        {
            get { return GetRepositoryLazy(this.instructorRepository); }
        }

        public GenericRepository<OfficeAssignment> OfficeAssignmentRepository
        {
            get { return GetRepositoryLazy(this.officeAssignmentRepository); }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
