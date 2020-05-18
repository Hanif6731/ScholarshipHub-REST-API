using ScholarshipHubRestApi.Attributes;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using ScholarshipHubRestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScholarshipHubRestApi.Controllers
{
    [RoutePrefix("api/student/{sId}/universityoffer")]
    public class OfferUniversityController : ApiController
    {
        IUniversityOfferRepository uniOfferRepo = new UniversityOfferRepository();

        [Route("")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult GetAll([FromUri] int sId)
        {
            var offers = uniOfferRepo.GetAll();
            foreach (UniversityOffer offer in offers)
            {
                linkGen(offer, sId);
            }
            return Ok(offers);
        }
        [Route("{oId}")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get([FromUri] int sId,[FromUri]int oId)
        {
            var offer = uniOfferRepo.Get(oId);
            linkGen(offer,sId);
            return Ok(offer);
        }
        [NonAction]
        public void linkGen(UniversityOffer offer,int sId)
        {
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/student/"+sId+"/universityoffer/", Method = "GET", Rel = "Get all the university offer list" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/universityoffer/" + offer.id, Method = "GET", Rel = "Get specific university offer" });
        }
    }
}
