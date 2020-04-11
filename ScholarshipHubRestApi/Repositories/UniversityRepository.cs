﻿using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScholarshipHubRestApi.Repository
{
    public class UniversityRepository : Repository<University>, IUniversityRepository
    {
        public University GetUniversity(string username)
        {
            return context.Set<University>().SingleOrDefault(u => u.username == username);
        }
    }
}