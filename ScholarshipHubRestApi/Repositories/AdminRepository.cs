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


        public Admin GetAdmin(string username)
        {
            return context.Set<Admin>().SingleOrDefault(u => u.username == username);
        }



        public Admin GetAdminByID(int id)
        {
            return context.Set<Admin>().SingleOrDefault(admin => admin.id ==id );
        }

        public IEnumerable<Admin> GetAdminByPayment()
        {
            return context.Set<Admin>().Where<Admin>(s => s.salarystatus == 0);
        }

        public Admin GetAdminByName(string name)
        {
            return context.Set<Admin>().SingleOrDefault(admin => admin.name == name);
        }

        object IAdminRepository.GetAdmin(string username)
        {
            return context.Set<Admin>().SingleOrDefault(u => u.username == username);
        }
    }
}