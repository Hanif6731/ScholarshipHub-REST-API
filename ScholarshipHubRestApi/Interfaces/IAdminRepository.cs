using ScholarshipHubRestApi.Models;
using ScholarshipHubRestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IAdminRepository: IRepository<Admin> 
    {
        new IEnumerable<Admin> GetAll();
        Admin GetAdminByID(string username);
        //Admin GetAdminByPayment();
        Admin GetAdminByName(string name);
        new Admin Get(int id);
        new void Insert(Admin entity);
        new void Update(Admin entity);
        new void Delete(int id);



    }
}
