using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IOrgMessegeRepository:IRepository<Messege>
    {
        IEnumerable<Messege> GetMessege(string email);
        IEnumerable<Messege> GetMessegeset(string email);
    }
}
