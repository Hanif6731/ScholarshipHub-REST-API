using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IUserRepository:IRepository<User>
    {
        int Get(User user);
        User GetUser(string username);
    }
}
