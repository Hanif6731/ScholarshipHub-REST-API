using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IUniversityRepository:IRepository<University>
    {
        University GetUniversity(string username);
        //new IEnumerable<University> GetAll();
        object GetUniversity(int id);
    }
}
