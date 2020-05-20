using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class UniversityOfferRepository : Repository<UniversityOffer>, IUniversityOfferRepository
    {
        public IEnumerable<UniversityOffer> GetAll(int id)
        {
            return context.Set<UniversityOffer>().Where(u => u.UniversityId == id).ToList();
        }

        public void getUniversityOfferByUid(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}