using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class ApplictionsToUniversityRepository : Repository<ApplictionsToUniversity>, IApplictionsToUniversityRepository
    {
        public void DeleteAll(int offerId)
        {
            context.Set<ApplictionsToUniversity>().RemoveRange(GetAll(offerId));
            context.SaveChanges();
        }

        public IEnumerable<ApplictionsToUniversity> GetAll(int uniOfferId)
        {
            return context.Set<ApplictionsToUniversity>().Where(u => u.UniversityOfferID == uniOfferId).ToList();
        }

        public IEnumerable<ApplictionsToUniversity> GetAllApplications(int UniversityId)
        {
            return context.Set<ApplictionsToUniversity>().Where(u => u.UniversityOffer.UniversityId == UniversityId).ToList();
        }

        public IEnumerable<ApplictionsToUniversity> GetStudentsApplicationToUniversity(int studentId)
        {
            return context.Set<ApplictionsToUniversity>().Where(u =>u.StudentId==studentId).ToList();
        }
        public ApplictionsToUniversity GetStudentsApplicationToUniversity(int studentId, int applicationId)
        {
            return context.Set<ApplictionsToUniversity>().SingleOrDefault(u => u.StudentId == studentId && u.id == applicationId);
        }
    }
}