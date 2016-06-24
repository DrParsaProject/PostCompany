using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PostCompany.Models;
using PostCompany.Forms;
using PostCompany.Utils;
using System.Web.SessionState;

namespace PostCompany.Controllers
{
    public class EmployeeLoginController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

        // POST api/EmployeeLogin
        public HttpResponseMessage PostEmployeeLogin(LoginForm form)
        {
			form.Password = Security.GetMd5Hash(form.Password);
			
			var user = (from e in db.Employees
					  where e.Username == form.Username && 
							e.Password == form.Password
					  select new {e.Id, e.Role}).SingleOrDefault();

			if (user == null)
				return Request.CreateResponse(HttpStatusCode.NotAcceptable);

			Authentication.Authenticate(user.Id, user.Role);
			return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}