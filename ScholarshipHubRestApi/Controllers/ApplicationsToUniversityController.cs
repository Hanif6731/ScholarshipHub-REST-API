using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using ScholarshipHubRestApi.Repository;

namespace ScholarshipHubRestApi.Controllers
{
    [RoutePrefix("api")]
    public class ApplicationsToUniversityController : ApiController
    {
        IApplictionsToUniversityRepository apRep = new ApplictionsToUniversityRepository();
        [Route("univerisityOffer/{oId}/applications",Name ="applicationsInOffer")]
        // GET api/<controller>
        public IHttpActionResult Get(int oId)
        {
            var applications = apRep.GetAll(oId);
            return Ok(applications);
        }
        [Route("students/{sId}/applications", Name = "applicationsOfStudent")]
        public IHttpActionResult GetApplications(int sId)
        {
            var applications = apRep.GetStudentsApplicationToUniversity(sId);
            return Ok(applications);
        }

        [Route("univerisityOffer/{oId}/applications/{id}", Name = "GetApplicationById")]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id, int oId)
        {
            var application = apRep.Get(id);
            return Ok(application);
        }

        [Route("univerisityOffer/{oId}/applications")]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ApplictionsToUniversity appliction, [FromUri]int oId)
        {
            appliction.UniversityOfferID = oId;
            apRep.Insert(appliction);
            string url = Url.Link("GetApplicationById", new { oId = appliction.UniversityOfferID, id = appliction.id });
            return Created(url, appliction);
        }

        [Route("univerisityOffer/{oId}/applications/{id}")]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromUri]int oId, [FromBody]ApplictionsToUniversity appliction)
        {
            apRep.Update(appliction);
            return Ok(appliction);

        }

        [Route("univerisityOffer/{oId}/applications/{id}")]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            apRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}