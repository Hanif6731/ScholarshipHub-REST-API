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
    [RoutePrefix("api/organisations/{uId}")]
    public class ApplicationsToOrganizationController : ApiController
    {
        IApplicationToOrganizationRepository apRep = new ApplicationToOrganizationRepository();
        [Route("Offers/{oId}/applications")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int oId)
        {
            var applications = apRep.GetAll(oId);
            foreach (ApplicationsToOrganization appliction in applications)
            {
                linkGen(appliction);
            }
            return Ok(applications);
        }

        [Route("applications")]
       [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult GetAllApplications(int uId)
        {

            var applications = apRep.GetStudentsApplicationToOrganization(uId);
            foreach (ApplicationsToOrganization appliction in applications)
            {
                linkGen(appliction);
            }
            return Ok(applications);
        }

        [Route("Offers/{oId}/applications/{id}", Name = "GetApplicationByIds")]
      //  [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id, int oId)
        {
            var application = apRep.Get(id);
            linkGen(application);
            return Ok(application);
        }

        [Route("Offers/{oId}/applications")]
        //[BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ApplicationsToOrganization appliction, [FromUri]int oId, [FromUri]int uId)
      {
            appliction.StudentId = uId;
          appliction.organizationsOfferID = oId;
          apRep.Insert(appliction);
          string url = Url.Link("GetApplicationByIds", new { oId = appliction.organizationsOfferID, id = appliction.id });
          return Created(url, appliction);
      }
      

        [Route("Offers/{oId}/applications/{id}")]
       // [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromUri]int oId, [FromBody]ApplicationsToOrganization appliction)
        {
            apRep.Update(appliction);
            linkGen(appliction);
            return Ok(appliction);

        }
        [Route("Offers/{oId}/applications/{id}")]
        //[BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                apRep.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [NonAction]
        public void linkGen(ApplicationsToOrganization appliction)
        {
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + appliction.OrganizationOffer.organization_id + "/applications", Method = "GET", Rel = "Get all the applications list to an organisations" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + appliction.OrganizationOffer.organization_id + "/offers/" + appliction.organizationsOfferID + "/applications", Method = "GET", Rel = "Get all the applications list to an scholarship offer of an university" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + appliction.OrganizationOffer.organization_id + "/offers/" + appliction.organizationsOfferID + "/applications/" + appliction.id, Method = "GET", Rel = "Get an specified application to an university offer by ID" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + appliction.OrganizationOffer.organization_id + "/offers/" + appliction.organizationsOfferID + "/applications" + appliction.id, Method = "PUT", Rel = "Modify an existing application resource" });
            appliction.links.Add(new Links() { HRef = "http://localhost:44348/api/organisations/" + appliction.OrganizationOffer.organization_id + "/offers/" + appliction.organizationsOfferID + "/applications", Method = "DELETE", Rel = "Delete an existing application resource" });
        }
    }
}
