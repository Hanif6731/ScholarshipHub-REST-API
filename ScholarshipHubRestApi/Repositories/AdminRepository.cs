using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System.Data.Entity;


namespace ScholarshipHubRestApi.Repository
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        //ScholarshipHubDataContext context;
        //private int salarystatus;

        
        

        public Admin GetAdminByID(string username)
        {
            return context.Set<Admin>().SingleOrDefault(admin => admin.username == username);
        }

        public IEnumerable<Admin> GetAdminByPayment()
        {
            return context.Set<Admin>().Where<Admin>(s => s.salarystatus == 0);
        }

        public Admin GetAdminByName(string name)
        {
            return context.Set<Admin>().SingleOrDefault(admin => admin.name == name);
        }

      
    }
}