﻿using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IMessegeRepository:IRepository<Messege>
    {
        IEnumerable<Messege> GetAll(string email);
    }
}
