using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;
using ScholarshipHubRestApi.Interfaces;
using ScholarshipHubRestApi.Repository;

namespace ScholarshipHubRestApi.Attributes
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string encoded = actionContext.Request.Headers.Authorization.Parameter.ToString();
                string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
                string[] arr = decoded.Split(new char[]{':'});
                string username = arr[0];
                string password = arr[1];
                IUserRepository uRep = new UserRepository();
                var user = uRep.GetUser(username);
                if (username == user.Username && password == user.Password)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("admin"), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}