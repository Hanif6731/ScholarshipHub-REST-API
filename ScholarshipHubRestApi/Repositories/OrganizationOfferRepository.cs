using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class OrganizationOfferRepository : Repository<OrganizationOffer>, IOrganizationOfferRepository
    {
        public IEnumerable<OrganizationOffer> GetAll(int id)
        {
            return context.Set<OrganizationOffer>().Where(u => u.organization_id == id).ToList();
        }
    }

}


