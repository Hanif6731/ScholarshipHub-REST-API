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
    [RoutePrefix("api/student/{sId}/university")]
    public class UniversityApplicationController : ApiController
    {
        IApplictionsToUniversityRepository appRepo = new ApplictionsToUniversityRepository();
        [Route("applications")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get([FromUri]int sId)
        {
            var applications = appRepo.GetStudentsApplicationToUniversity(sId);
            foreach (ApplictionsToUniversity application in applications)
            {
                linkGen(application,sId);
            }
            return Ok(applications);
        }

        [Route("{oId}/application")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Post(int sId,[FromUri] int oId, [FromBody]ApplictionsToUniversity application)
        {
            application.UniversityOfferID = oId;
            application.StudentId = sId;
            appRepo.Insert(application);
            linkGen(application,sId);
            return Created("",application);
        }

        [Route("application/{appId}")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int sId,int appId)
        {
            var application = appRepo.GetStudentsApplicationToUniversity(sId, appId);
            linkGen(application,sId);
            return Ok(application);
        }

        [Route("application/{appId}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri] int sId, [FromUri]int appId, [FromBody]ApplictionsToUniversity application)
        {
            application.id = appId;
            appRepo.Update(application);
            linkGen(application,sId);
            return Ok(application);
        }

        [Route("application/{appId}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult delete([FromUri] int sId, [FromUri]int appId)
        {
            appRepo.Delete(appId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [NonAction]
        public void linkGen(ApplictionsToUniversity app, int sId)
        {
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/university/applications/", Method = "GET", Rel = "Get all the application to university by specific student" });
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/university/application/" + app.id, Method = "GET", Rel = "Get specific application" });
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/university/application/" + app.id, Method = "PUT", Rel = "update specific application" });
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/university/application/" + app.id, Method = "delete", Rel = "delete specific application" });
        }
    }
}
