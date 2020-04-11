using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class StudentRepository: Repository<Student>,IStudentRepository
    {
        public Student GetStudent(string username)
        {
            return context.Set<Student>().SingleOrDefault(s => s.Username == username);
        }
        
 
    }
}