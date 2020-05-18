using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IOrganizationOfferRepository:IRepository<OrganizationOffer>
    {
        IEnumerable<OrganizationOffer> GetAll(int id);
    }
}
