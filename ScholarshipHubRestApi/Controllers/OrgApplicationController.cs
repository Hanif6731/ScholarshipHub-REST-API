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
    [RoutePrefix("api/student/{sId}/org")]
    public class OrgApplicationController : ApiController
    {
        IApplicationsToOrganizationRepository appRepo = new ApplicationsToOrganiztionRepository();
        [Route("applications")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int sId)
        {
            var applications = appRepo.GetStudentsApplicationToOrganization(sId);
            foreach (ApplicationsToOrganization application in applications)
            {
                linkGen(application,sId);
            }
            return Ok(applications);
        }

        [Route("{oId}/application")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Post(int sId, [FromUri] int oId, [FromBody]ApplicationsToOrganization application)
        {
            application.organizationsOfferID = oId;
            application.StudentId = sId;
            appRepo.Insert(application);
            linkGen(application, sId);
            return Created("", application);
        }

        [Route("application/{appId}")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(int sId, int appId)
        {
            var app = appRepo.GetStudentsApplicationToOrganization(sId, appId);
            linkGen(app, sId);
            return Ok(app);
        }

        [Route("application/{appId}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri] int sId, [FromUri]int appId, [FromBody]ApplicationsToOrganization application)
        {
            application.id = appId;
            application.StudentId = sId;
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
        public void linkGen(ApplicationsToOrganization app, int sId)
        {
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/org/applications/", Method = "GET", Rel = "Get all the application to org by specific student" });
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/org/application/" + app.id, Method = "GET", Rel = "Get specific application" });
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/org/application/" + app.id, Method = "PUT", Rel = "update specific application" });
            app.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + sId + "/org/application/" + app.id, Method = "delete", Rel = "delete specific application" });
        }
    }
}
