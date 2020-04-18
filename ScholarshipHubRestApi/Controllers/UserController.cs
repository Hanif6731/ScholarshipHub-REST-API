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
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        IUserRepository uRep = new UserRepository();
        // GET api/<controller>
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = uRep.GetAll();

            if (users.Count() > 0)
            {
                return Ok(users);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("user", Name ="GetUserByUsername")]
        // GET api/<controller>/5
        public IHttpActionResult Get(User user_)
        {
            var user = uRep.GetUser(user_.Username);

            if (uRep.Get(user) == 1)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("")]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]User user)
        {
            uRep.Insert(user);
            string url = Url.Link("GetUserByUsername", new { user });
            return Created(url, user);
        }

        [Route("id")]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]User user, [FromUri]int id)
        {
            user.id = id;
            uRep.Update(user);

            return Ok(user);
        }

        [Route("id")]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            uRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}
