using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public int Get(User user)
        {
            return base.context.Set<User>().Where(u => u.Username == user.Username && u.Password == user.Password).ToList().Count();
        }

        public User GetUser(string username)
        {
            return base.context.Set<User>().SingleOrDefault(u => u.Username == username );
        }
    }
}