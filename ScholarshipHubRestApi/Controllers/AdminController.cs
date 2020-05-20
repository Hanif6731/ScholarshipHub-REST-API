using ScholarshipHubRestApi.Attributes;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Models;
using ScholarshipHubRestApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http;

namespace ScholarshipHubRestApi.Controllers
{
    [RoutePrefix("api/admins")]
    public class AdminController : ApiController
    {
        IUserRepository userRepo = new UserRepository();
        IAdminRepository admRepo = new AdminRepository();
        IStudentRepository stdRepo = new StudentRepository();
        IUniversityOfferRepository uoRepo = new UniversityOfferRepository();
        IUniversityRepository uRepo = new UniversityRepository();

        IApplictionsToUniversityRepository auRepo = new ApplictionsToUniversityRepository();



        [Route("")]
        //[BasicAuthentication]
        public IHttpActionResult Get()
        {
            var admins = admRepo.GetAll();
            var filePath = HttpContext.Current.Server.MapPath("~/Media/Files");

            foreach (Admin admin in admins)
            {
                linkGen(admin);
                //admin.ApprovalPath = filePath + "/" + admin.ApprovalPath;
            }

            return Ok(admins);
        }


        [Route("{username}/", Name = "GetAdminByName")]
        [BasicAuthentication]
        // GET api/<controller>/5
        public IHttpActionResult Get(string username)
        {
            Admin admin = (Admin)admRepo.GetAdmin(username);
            //var filePath = HttpContext.Current.Server.MapPath("~/Media/Files");
            //filePath = filePath.Replace("\\", "/");
            //university.ApprovalPath = filePath + "/" + university.ApprovalPath;
            linkGen(admin);
            return Ok(admin);
        }


        [Route("")]
        //[BasicAuthentication]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Admin admin)
        {
            var user = new User();
            user.Username = admin.username;
            user.Password = admin.password;
            user.Status = 0;
            admin.balance = 0;
            userRepo.Insert(user);
            admRepo.Insert(admin);
            linkGen(admin);
            string url = Url.Link("GetAdminByName", new { username = admin.username });
            return Created(url, admin);
        }

        [Route("{username}/payment")]
        //[BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Post([FromUri]string username)
        {
            Admin myadmin = (Admin)admRepo.GetAdmin(username);
            myadmin.salarystatus = 1;
            myadmin.balance += myadmin.salary;            
            admRepo.Update(myadmin);
            linkGen(myadmin);
            return Ok(myadmin);


        }

        [Route("{id}/withdraw/{amount}")]
        //[BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult Get([FromUri]int id, [FromUri]int amount)
        {
            Admin myadmin = (Admin)admRepo.GetAdminByID(id);
            if(amount<=myadmin.balance)
            {
                myadmin.balance = myadmin.balance - amount;
                admRepo.Update(myadmin);
                var msg = "Balance Withdrawn";
                return Ok(msg);

            }
            else
            {
                var msg = "Insufficient Balance";
                return Ok(msg);


            }
            


        }

        [Route("student/{sid}/applications")]
        //[BasicAuthentication]

        public IHttpActionResult GetStudentOffers([FromUri]int sid)
        {
            var applictionsToUniversity = auRepo.GetStudentsApplicationToUniversity(sid);
            return Ok(applictionsToUniversity);
        }

        [Route("uni/{id}/offers")]
        //[BasicAuthentication]
        // PUT api/<controller>/5
        public IHttpActionResult GetUniOffer([FromUri]int id)
        {
            var offers = uoRepo.GetAll(id);
            
            return Ok(offers);
        }

        private void linkGen(UniversityOffer offer)
        {
            throw new NotImplementedException();
        }

        [Route("uni/", Name = "GetUni")]
        public IHttpActionResult GetUniversities()
        {
            var universities = uRepo.GetAll();
            var filePath = HttpContext.Current.Server.MapPath("~/Media/Files");

            foreach (University university in universities)
            {
                linkGen(university);
                university.ApprovalPath = filePath + "/" + university.ApprovalPath;
                User user = (User)userRepo.GetUser(university.username);
                if (user.Status == 5)
                {
                    //university.Current_Situation = "Banned";
                }
                else
                {
                    //university.Current_Situation = "On work";
                }
            }

            return Ok(universities);
        }

        [Route("uni/{id}", Name = "DeleteUni")]
        //[BasicAuthentication]
        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            University university = (University)uRepo.GetUniversity(id);
            try
            {
                User user = (User)userRepo.GetUser(university.username);
                user.Status = 5;//Banned
                userRepo.Update(user);
                return Ok(user);
            }
            catch(Exception e)
            {
                var msg = e.Message;
                return Ok(msg);
            }

            
            
        }



        private IHttpActionResult StatusCode(object noContent)
        {
            throw new NotImplementedException();
        }

        [NonAction]
        public void linkGen(Admin admin)
        {
            admin.links.Add(new Links() { HRef = "https://localhost:44348/api/admins", Method = "GET", Rel = "Get all the admins list" });
            admin.links.Add(new Links() { HRef = "https://localhost:44348/api/admins/" + admin.username + "/", Method = "GET", Rel = "Get an admin user by username" });
            admin.links.Add(new Links() { HRef = "https://localhost:44348/api/admins", Method = "POST", Rel = "Create a new admin resource" });
            admin.links.Add(new Links() { HRef = "https://localhost:44348/api/admins/" + admin.id, Method = "PUT", Rel = "Modify an existing admin resource" });
            admin.links.Add(new Links() { HRef = "https://localhost:44348/api/admins/" + admin.id, Method = "DELETE", Rel = "Delete an existing admin resource" });

        }

        [NonAction]
        public void linkGen(University university)
        {
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/admins/uni", Method = "GET", Rel = "Get all the universities list" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/admins/uni/" + university.username + "/", Method = "GET", Rel = "Get an university user by username" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/admins/uni/", Method = "POST", Rel = "Create a new university resource" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/admins/uni/" + university.id, Method = "PUT", Rel = "Modify an existing uinversity resource" });
            university.links.Add(new Links() { HRef = "http://localhost:44348/api/admins/uni/" + university.id, Method = "DELETE", Rel = "Delete an existing university resource" });
        }
    }
}
