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

namespace PostCompany.Controllers
{
    public class CustomerLoginController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

		// POST api/CustomerLogin
		public HttpResponseMessage PostCustomerLogin(LoginIForm form)
		{
			form.Password = Security.GetMd5Hash(form.Password);

			int id = (from e in db.Customers
						where e.Username == form.Username &&
							  e.Password == form.Password
						select e.Id).SingleOrDefault();

			if (id == 0)
				return Request.CreateResponse(HttpStatusCode.NotAcceptable);

			Authentication.AuthenticateCustomer(id);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}