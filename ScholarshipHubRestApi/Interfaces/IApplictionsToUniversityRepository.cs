using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IApplictionsToUniversityRepository : IRepository<ApplictionsToUniversity>
    {
        IEnumerable<ApplictionsToUniversity> GetAll(int uniOfferId);
        IEnumerable<ApplictionsToUniversity> GetAllApplications(int UniversityId);
        IEnumerable<ApplictionsToUniversity> GetStudentsApplicationToUniversity(int studentId);
    }
}
