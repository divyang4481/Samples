using System;
using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.DAL
{
    [Obsolete("This class is not used")]
    public interface IStudentRepository : IDisposable
    {
        IQueryable<Student> GetStudents();
        Student GetStudentById(int studentId);
        void InsertStudent(Student student);
        void DeleteStudent(int studentId);
        void UpdateStudent(Student student);
        void Save();
    }
}