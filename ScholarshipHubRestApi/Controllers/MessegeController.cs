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
    [RoutePrefix("api/messeges")]
    public class MessegeController : ApiController
    {
        IMessegeRepository msgRep = new MessegeRepository();
        [Route("{fromUserEmail}/")]
        [BasicAuthentication]
        // GET api/<controller>
        public IHttpActionResult Get(string fromUserEmail)
        {
            var msgs = msgRep.GetAll(fromUserEmail);
            foreach (Messege msg in msgs)
            {
                linkGen(msg);
            }
            return Ok(msgs);
        }

        [Route("{id}", Name = "GetMessegeById")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var msg = msgRep.Get(id);
            linkGen(msg);
            return Ok(msg);
        }

        [Route("")]
        [BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Messege messege)
        {
            msgRep.Insert(messege);
            linkGen(messege);
            string url = Url.Link("GetMessegeById", new { id = messege.id });
            return Created(url, messege);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromBody]Messege messege)
        {
            
            messege.id = id;
            msgRep.Update(messege);
            linkGen(messege);
            return Ok(messege);
        }
        [Route("{id}")]
        [BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            msgRep.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [NonAction]
        public void linkGen(Messege msg)
        {
            msg.links.Add(new Links() { HRef = "http://localhost:44348/api/messeges/" + msg.FromUser+"/", Method = "GET", Rel = "Get all the messege list of an user" });
            msg.links.Add(new Links() { HRef = "http://localhost:44348/api/messeges/" + msg.id, Method = "GET", Rel = "Get an specific messege resource" });
            msg.links.Add(new Links() { HRef = "http://localhost:44348/api/messeges" , Method = "POST", Rel = "Create a new messege resource" });
            msg.links.Add(new Links() { HRef = "http://localhost:44348/api/messeges/" + msg.id , Method = "PUT", Rel = "Modify an existing messege resource" });
            msg.links.Add(new Links() { HRef = "http://localhost:44348/api/messeges/" + msg.id, Method = "DELETE", Rel = "Delete an existing messege resource" });
        }
    }
}