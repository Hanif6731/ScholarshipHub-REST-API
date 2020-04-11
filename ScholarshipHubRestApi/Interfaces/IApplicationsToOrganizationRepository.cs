using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IApplicationsToOrganizationRepository : IRepository<ApplicationsToOrganization>
    {
        IEnumerable<ApplicationsToOrganization> GetAll(int uniId);
        IEnumerable<ApplicationsToOrganization> GetStudentsApplicationToOrganization(int id);
    }
}
