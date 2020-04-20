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
        // GET api/<controller>
        public IHttpActionResult Get(int uId)
        {
            var offers = offerRep.GetAll(uId);
            return Ok(offers);
        }

        [Route("{id}",Name ="GetOfferById")]
        // GET api/<controller>/5
        public IHttpActionResult Get(int uId, int id)
        {
            var offer = offerRep.Get(id);
            return Ok(offer);
        }

        [Route("")]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]UniversityOffer universityOffer,[FromUri]int uId)
        {
            universityOffer.UniversityId = uId;
            offerRep.Insert(universityOffer);
            string url = Url.Link("GetOfferById", new {uId=universityOffer.UniversityId,id=universityOffer.id });
            return Created(url, universityOffer);
        }

        [Route("{id}")]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri] int uId,[FromUri]int id, [FromBody]UniversityOffer universityOffer)
        {
            universityOffer.UniversityId = uId;
            universityOffer.id = id;
            offerRep.Update(universityOffer);
            return Ok(universityOffer);
        }
        [Route("{id}")]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            offerRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
