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
    [RoutePrefix("api/universities")]
    public class UniversityController : ApiController
    {
        IUserRepository uRep = new UserRepository();
        IUniversityRepository uniRep = new UniversityRepository();

        [Route("")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var universities = uniRep.GetAll();
            var filePath = HttpContext.Current.Server.MapPath("~/Media/Files");

            foreach (University university in universities)
            {
                linkGen(university);
                university.ApprovalPath = filePath + "/" + university.ApprovalPath;
            }

            return Ok(universities);
        }

        [Route("{username}/",Name ="GetUniByUsername")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(string username)
        {
            var university = uniRep.GetUniversity(username);
            //var filePath = HttpContext.Current.Server.MapPath("~/Media/Files");
            //filePath = filePath.Replace("\\", "/");
            //university.ApprovalPath = filePath + "/" + university.ApprovalPath;
            linkGen(university);
            return Ok(university);
        }

        [Route("")]
        //[BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]University university)
        {
            var user = new User();
            user.Username = university.username;
            user.Password = university.password;
            user.Status = 2;
            uRep.Insert(user);
            uniRep.Insert(university);
            linkGen(university);
            string url = Url.Link("GetUniByUsername", new { username = university.username });
            return Created(url, university);
        }
        [Route("{id}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromBody]University university)
        {
            university.id = id;
            uniRep.Update(university);
            linkGen(university);
            return Ok(university);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            uniRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [NonAction]
        public void linkGen(University university)
        {
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/universities", Method = "GET", Rel = "Get all the universities list" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + university.username+"/", Method = "GET", Rel = "Get an university user by username" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/universities", Method = "POST", Rel = "Create a new university resource" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + university.id, Method = "PUT", Rel = "Modify an existing uinversity resource" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/universities/" + university.id, Method = "DELETE", Rel = "Delete an existing university resource" });
        }
    }
}
