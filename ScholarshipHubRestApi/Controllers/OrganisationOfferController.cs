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
    [RoutePrefix("api/organisations/{uId}/offers")]
    public class OrganisationOfferController : ApiController
    {
        IOrganizationOfferRepository offerRep = new OrganizationOfferRepository();
        [Route("")]
       // [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int uId)
        {
            var offers = offerRep.GetAll(uId);
            foreach (OrganizationOffer offer in offers)
            {
                linkGen(offer);
            }
            return Ok(offers);
        }
        [Route("{id}", Name = "GetOfferByIds")]
       //[BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(int uId, int id)
        {
            var offer = offerRep.Get(id);
            linkGen(offer);
            return Ok(offer);
        }
        [Route("")]
       // [BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]OrganizationOffer organizationOffer, [FromUri]int uId)
        {
            organizationOffer.organization_id = uId;
            offerRep.Insert(organizationOffer);
            linkGen(organizationOffer);
            string url = Url.Link("GetOfferByIds", new { uId = organizationOffer.organization_id, id = organizationOffer.id });
            return Created(url, organizationOffer);
        }
        [Route("{id}")]
        //[BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri] int uId, [FromUri]int id, [FromBody]OrganizationOffer organizationOffer)
        {
            organizationOffer.organization_id = uId;
            organizationOffer.id = id;
            offerRep.Update(organizationOffer);
            linkGen(organizationOffer);
            return Ok(organizationOffer);
        }
        [Route("{id}")]
        //[BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            offerRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }



        [NonAction]
        public void linkGen(OrganizationOffer offer)
        {
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + offer.organization_id + "/offers", Method = "GET", Rel = "Get all the Scholarship offer list offered by a organisations" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + offer.organization_id + "/offers/" + offer.id, Method = "GET", Rel = "Get an specified Scholarship offer by ID" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + offer.organization_id + "/offers", Method = "POST", Rel = "Create a new Scholarship offer resource" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + offer.organization_id + "/offers/" + offer.id, Method = "PUT", Rel = "Modify an existing Scholarship offer resource" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + offer.organization_id + "/offers/" + offer.id, Method = "DELETE", Rel = "Delete an existing Scholarship offer resource" });
        }
    }
}
