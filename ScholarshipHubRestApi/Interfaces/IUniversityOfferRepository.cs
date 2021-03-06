﻿using ScholarshipHubRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipHubRestApi.Interfaces
{
    interface IUniversityOfferRepository:IRepository<UniversityOffer>
    {
        IEnumerable<UniversityOffer> GetAll(int id);
        void getUniversityOfferByUid(int id);

    }
}
