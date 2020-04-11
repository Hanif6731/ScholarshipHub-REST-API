using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class OrganisationRepository : Repository<Organisation>, IOrganisationRepository
    {
        public Organisation GetOrganisation(string username)
        {
            return base.context.Set<Organisation>().SingleOrDefault(u => u.Username == username);
        }

        public void UpdatePersonal(Organisation organisation)
        {
           
            context.Entry(organisation).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}