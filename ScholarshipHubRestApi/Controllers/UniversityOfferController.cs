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
    [RoutePrefix("api/universities/{uId}/offers")]
    public class UniversityOfferController : ApiController
    {
        IUniversityOfferRepository offerRep = new UniversityOfferRepository();
        [Route("")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int uId)
        {
            var offers = offerRep.GetAll(uId);
            foreach(UniversityOffer offer in offers)
            {
                linkGen(offer);
            }
            return Ok(offers);
        }

        [Route("{id}",Name ="GetOfferById")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(int uId, int id)
        {
            var offer = offerRep.Get(id);
            linkGen(offer);
            return Ok(offer);
        }

        [Route("")]
        [BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]UniversityOffer universityOffer,[FromUri]int uId)
        {
            universityOffer.UniversityId = uId;
            offerRep.Insert(universityOffer);
            linkGen(universityOffer);
            string url = Url.Link("GetOfferById", new {uId=universityOffer.UniversityId,id=universityOffer.id });
            return Created(url, universityOffer);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri] int uId,[FromUri]int id, [FromBody]UniversityOffer universityOffer)
        {
            universityOffer.UniversityId = uId;
            universityOffer.id = id;
            offerRep.Update(universityOffer);
            linkGen(universityOffer);
            return Ok(universityOffer);
        }
        [Route("{id}")]
        [BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            offerRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [NonAction]
        public void linkGen(UniversityOffer offer)
        {
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + offer.UniversityId + "/offers", Method = "GET", Rel = "Get all the Scholarship offer list offered by a university" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + offer.UniversityId + "/offers/" + offer.id, Method = "GET", Rel = "Get an specified Scholarship offer by ID" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + offer.UniversityId + "/offers", Method = "POST", Rel = "Create a new Scholarship offer resource" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + offer.UniversityId + "/offers/" + offer.id, Method = "PUT", Rel = "Modify an existing Scholarship offer resource" });
            offer.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + offer.UniversityId + "/offers/" + offer.id, Method = "DELETE", Rel = "Delete an existing Scholarship offer resource" });
        }
    }
}
