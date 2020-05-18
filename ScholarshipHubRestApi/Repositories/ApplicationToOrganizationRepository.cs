using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class ApplicationToOrganizationRepository : Repository<ApplicationsToOrganization>, IApplicationToOrganizationRepository
    {
        public IEnumerable<ApplicationsToOrganization> GetApp()
        {
            return context.Set<ApplicationsToOrganization>().Where(x => x.AplicationStatus == null);
        }

        public IEnumerable<ApplicationsToOrganization> GetApprove()
        {
            return context.Set<ApplicationsToOrganization>().Where(x => x.AplicationStatus == 1);
        }
        public IEnumerable<ApplicationsToOrganization> GetAll(int uniOfferId)
        {
            return context.Set<ApplicationsToOrganization>().Where(u => u.organizationsOfferID == uniOfferId).ToList();
        }

        public IEnumerable<ApplicationsToOrganization> GetStudentsApplicationToOrganization(int studentId)
        {
            return context.Set<ApplicationsToOrganization>().Where(u => u.StudentId == studentId).ToList();
        }
    }



}