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
    [RoutePrefix("api/users")][BasicAuthentication]
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
                foreach(User user in users)
                {
                    linkGen(user);
                }

                return Ok(users);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("{username}/", Name ="GetUserByUsername")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(string username)
        {
            var user = uRep.GetUser(username);

            if (uRep.Get(user) == 1)
            {
                linkGen(user);
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
            linkGen(user);
            string url = Url.Link("GetUserByUsername", new { username = user.Username });
            return Created(url, user);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]User user, [FromUri]int id)
        {
            user.id = id;
            uRep.Update(user);
            linkGen(user);

            return Ok(user);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            uRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);

        }

        [NonAction]
        public void linkGen(User user)
        {
            user.links.Add(new Links() { HRef = "http://localhost:44348/api/users", Method = "GET", Rel = "Get all the user list" });
            user.links.Add(new Links() { HRef = "http://localhost:44348/api/users/"+user.Username, Method = "GET", Rel = "Get an specified user by username" });
            user.links.Add(new Links() { HRef = "http://localhost:44348/api/users", Method = "POST", Rel = "Create a new user resource" });
            user.links.Add(new Links() { HRef = "http://localhost:44348/api/users/" + user.id, Method = "PUT", Rel = "Modify an existing user resource" });
            user.links.Add(new Links() { HRef = "http://localhost:44348/api/users/" + user.id, Method = "DELETE", Rel = "Delete an existing user resource" });
        }
    }
}
