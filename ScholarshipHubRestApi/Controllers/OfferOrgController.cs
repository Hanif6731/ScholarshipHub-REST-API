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
    [RoutePrefix("api/student/{sId}/orgoffer")]
    public class OfferOrgController : ApiController
    {
        IOrganizationOfferRepository offerRepo = new OrganizationOfferRepository();
        [Route("")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get([FromUri]int sId)
        {
            var offers = offerRepo.GetAll();
            foreach (OrganizationOffer offer in offers)
            {
                linkGen(offer,sId);
            }
            return Ok(offers);
        }
        [Route("{oId}")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get([FromUri]int sId,[FromUri]int oId)
        {
            var offer = offerRepo.Get(oId);
            linkGen(offer,sId);
            return Ok(offer);
        }
        [NonAction]
        public void linkGen(OrganizationOffer offer,int sId)
        {
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/student/"+sId+"/orgoffer/", Method = "GET", Rel = "Get all the org offer list" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/orgoffer/" + offer.id, Method = "GET", Rel = "Get specific org offer" });
        }
    }
}
