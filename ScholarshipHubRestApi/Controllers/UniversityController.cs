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
    [RoutePrefix("api/universities")]
    public class UniversityController : ApiController
    {
        IUserRepository uRep = new UserRepository();
        IUniversityRepository uniRep = new UniversityRepository();

        [Route("")]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var universities = uniRep.GetAll();

            return Ok(universities);
        }

        [Route("{username}/",Name ="GetUniByUsername")]
        // GET api/<controller>/5
        public IHttpActionResult Get(string username)
        {
            var university = uniRep.GetUniversity(username);
            return Ok(university);
        }

        [Route("")]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]University university)
        {
            
            var user = new User();
            user.Username = university.username;
            user.Password = university.password;
            user.Status = 2;
            uRep.Insert(user);
            uniRep.Insert(university);
            string url = Url.Link("GetUniByUsername", new { username = university.username });
            return Created(url, university);
        }
        [Route("{id}")]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromBody]University university)
        {
            university.id = id;
            uniRep.Update(university);
            return Ok(university);
        }

        [Route("{id}")]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            uniRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
