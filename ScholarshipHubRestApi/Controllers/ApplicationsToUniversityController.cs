using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScholarshipHubRestApi.Attributes;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using ScholarshipHubRestApi.Repository;

namespace ScholarshipHubRestApi.Controllers
{
    [RoutePrefix("api/universities/{uId}")]
    public class ApplicationsToUniversityController : ApiController
    {
        IApplictionsToUniversityRepository apRep = new ApplictionsToUniversityRepository();
        [Route("Offers/{oId}/applications")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int uId,int oId)
        {
            var applications = apRep.GetAll(oId);
            foreach(ApplictionsToUniversity appliction in applications)
            {
                linkGen(appliction,uId);
            }
            return Ok(applications);
        }

        [Route("applications")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult GetAllApplications(int uId)
        {
            
            var applications = apRep.GetAllApplications(uId);
            foreach (ApplictionsToUniversity appliction in applications)
            {
                linkGen(appliction,uId);
            }
            return Ok(applications);
        }
        

        [Route("Offers/{oId}/applications/{id}", Name = "GetApplicationById")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id, int oId,int uId)
        {
            var application = apRep.Get(id);
            linkGen(application,uId);
            return Ok(application);
        }

        /*
        [Route("")]
        [BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ApplictionsToUniversity appliction, [FromUri]int oId)
        {
            appliction.UniversityOfferID = oId;
            apRep.Insert(appliction);
            string url = Url.Link("GetApplicationById", new { oId = appliction.UniversityOfferID, id = appliction.id });
            return Created(url, appliction);
        }
        */

        [Route("Offers/{oId}/applications/{id}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ApplictionsToUniversity appliction, [FromUri]int id, [FromUri]int oId, [FromUri]int uId)
        {
            appliction.id = id;
            apRep.Update(appliction);
            linkGen(appliction,uId);
            return Ok(appliction);

        }

        [Route("Offers/{oId}/applications/{id}")]
        [BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            apRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [NonAction]
        public void linkGen(ApplictionsToUniversity appliction,int uniId)
        {
            
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" +uniId + "/applications", Method = "GET", Rel = "Get all the applications list to an university" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + uniId + "/offers/" + appliction.UniversityOfferID + "/applications", Method = "GET", Rel = "Get all the applications list to an scholarship offer of an university" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + uniId + "/offers/" + appliction.UniversityOfferID + "/applications/"+appliction.id, Method = "GET", Rel = "Get an specified application to an university offer by ID" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + uniId + "/offers/" + appliction.UniversityOfferID + "/applications"+appliction.id, Method = "PUT", Rel = "Modify an existing application resource" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + uniId + "/offers/" + appliction.UniversityOfferID + "/applications", Method = "DELETE", Rel = "Delete an existing application resource" });
        }
    }
}