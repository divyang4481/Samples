using ContosoUniversity.Models;
using System;
using System.Collections.Generic;

namespace ContosoUniversity.DAL
{
    public interface IUnitOfWork //: IDisposable
    {
        GenericRepository<Department> DepartmentRepository { get; }
        GenericRepository<Course> CourseRepository { get; }
        GenericRepository<Student> StudentRepository { get; }
        GenericRepository<Instructor> InstructorRepository { get; }
        GenericRepository<OfficeAssignment> OfficeAssignmentRepository { get; }
        void Save();        
    }
}