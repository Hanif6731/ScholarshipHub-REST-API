using ScholarshipHubRestApi.Attributes;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using ScholarshipHubRestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ScholarshipHubRestApi.Controllers
{
  
    [RoutePrefix("api/organisations")]
    public class OrganisationController : ApiController
    {
        IUserRepository uRep = new UserRepository();
        IOrganisationRepository orgRep = new OrganisationRepository();

        [Route("")]
        //[BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var organisations = orgRep.GetAll();
            var filePath = HttpContext.Current.Server.MapPath("~/Media/Files");

            foreach (Organisation organisation in organisations)
            {
                linkGen(organisation);
                organisation.ApprovalPath = filePath + "/" + organisation.ApprovalPath;
            }

            return Ok(organisations);
        }
      // [BasicAuthentication]
        [Route("{username}/", Name = "GetUniByUsernames")]
        
        // GET api/<controller>/5
        public IHttpActionResult Get(string username)
        {
            var organisation = orgRep.GetOrganisation(username);
            linkGen(organisation);
            return Ok(organisation);
        }
       
        [Route("")]
       
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Organisation organisation)
        {

            var user = new User();
            user.Username = organisation.Username;
            user.Password = organisation.Password;
            organisation.Information = "bcwg";
            user.Status = 3;
            orgRep.Insert(organisation);
            uRep.Insert(user);
            
            linkGen(organisation);
            string url = Url.Link("GetUniByUsernames", new { username = organisation.Username });
            return Created(url, organisation);
        }
        [Route("{id}")]
       
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromBody]Organisation organisation)
        {
            organisation.id = id;
            orgRep.Update(organisation);
            linkGen(organisation);
            return Ok(organisation);
        }

        [Route("{id}")]
    
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            orgRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }




        [NonAction]
        public void linkGen(Organisation organisation)
        {
            organisation.links.Add(new Links() { HRef = "http://localhost:44348/api/organisation", Method = "GET", Rel = "Get all the organisation list" });
            organisation.links.Add(new Links() { HRef = "http://localhost:44348/api/organisation/" + organisation.Username + "/", Method = "GET", Rel = "Get an organisation user by username" });
            organisation.links.Add(new Links() { HRef = "http://localhost:44348/api/organisation", Method = "POST", Rel = "Create a new organisation resource" });
            organisation.links.Add(new Links() { HRef = "http://localhost:44348/api/organisation/" + organisation.id, Method = "PUT", Rel = "Modify an existing organisation resource" });
            organisation.links.Add(new Links() { HRef = "http://localhost:44348/api/organisation/" + organisation.id, Method = "DELETE", Rel = "Delete an existing organisation resource" });
        }
    }
}
