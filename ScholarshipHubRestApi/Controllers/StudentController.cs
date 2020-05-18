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
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        IUserRepository uRep = new UserRepository();
        IStudentRepository studentRepo = new StudentRepository();
       /*
        [Route("{username}/", Name = "GetStudentByUsername")][BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(string username)
        {
            var student = studentRepo.GetStudent(username);
            linkGen(student);
            return Ok(student);
        }*/

        [Route("{sId}/", Name = "GetStudentById")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(int sId)
        {
            var student = studentRepo.Get(sId);
            linkGen(student);
            return Ok(student);
        }

        [Route("")]
        //[BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Student student)
        {
            var user = new User();
            user.Username = student.Username;
            user.Password = student.Password;
            user.Status = 1;
            uRep.Insert(user);
            studentRepo.Insert(student);
            linkGen(student);
            string url = Url.Link("GetStudentById", new { sId = student.id });
            return Created(url, student);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Put([FromUri]int id, [FromBody]Student student)
        {
            student.id = id;
            studentRepo.Update(student);
            linkGen(student);
            return Ok(student);
        }

        [Route("{id}")]
        [BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            studentRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [NonAction]
        public void linkGen(Student student)
        {
            student.links.Add(new Links() { HRef = "http://localhost:44348/api/student/"+student.Username+"/" , Method = "GET", Rel = "Get student user by username" });
            student.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + student.id + "/", Method = "GET", Rel = "Get student user by id" });
            student.links.Add(new Links() { HRef = "http://localhost:44348/api/student", Method = "POST", Rel = "Create a new student resource" });
            student.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + student.id, Method = "PUT", Rel = "Modify an existing student resource" });
            student.links.Add(new Links() { HRef = "http://localhost:44348/api/student/" + student.id, Method = "DELETE", Rel = "Delete an existing student resource" });
    
    }



    }
}
