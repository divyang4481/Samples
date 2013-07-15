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

        private GenericRepositoryLazy<T> GetRepository(T repository)
            where T : new()
        {
            if(repository == null)
            {
                repository = new T();
            }
            
            return repository;
        }

        public GenericRepository<Department> DepartmentRepository
        {
            get { return GetRepositoryLazy(this.departmentRepository); }
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
