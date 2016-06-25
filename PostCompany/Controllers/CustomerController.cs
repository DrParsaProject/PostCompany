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
using PostCompany.Utils;
using PostCompany.OutputForms;
using PostCompany.InputForms;

namespace PostCompany.Controllers
{
    public class CustomerController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

        // GET api/Customer/5
		public CustomerProfileOForm GetCustomer(int id)
		{
			if (!Authorize.hasRole(EmployeeRole.Counter) && !Authorize.isCurrentUser(id, UserType.Customer))
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);

			Customer customer = db.Customers.Find(id);
			if (customer == null)
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

			return new CustomerProfileOForm(customer);
		}

		// PUT api/Customer/5
		public HttpResponseMessage PutCustomer(int id, EditCustomerIForm form)
		{
			if (!Authorize.hasRole(EmployeeRole.Counter) && !Authorize.isCurrentUser(id, UserType.Customer))
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);

			Customer c = db.Customers.Find(id);
			if (c == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			if (form.Name != null)
				c.Name = form.Name;
			if (form.Phone != null)
				c.Phone = form.Phone;
			if (form.Address != null)
				c.Address = form.Address;
			if (form.OldPassword != null && form.NewPassword != null)
			{
				if (Security.VerifyMd5Hash(form.OldPassword, c.Password))
					c.Password = Security.GetMd5Hash(form.NewPassword);
				else
					throw new HttpResponseException(HttpStatusCode.NotAcceptable);
			}

			db.Entry(c).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
			}

			return Request.CreateResponse(HttpStatusCode.OK);
		}

		// POST api/Customer
		public HttpResponseMessage PostCustomer(RegisterCustomerIForm form)
		{
			if (!Authorize.hasRole(EmployeeRole.Counter))
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);

			Customer c = new Customer();
			c.Username = form.Username;
			c.Password = Security.GetMd5Hash(form.Password);
			c.Name = form.Name;

			if (ModelState.IsValid)
			{
				db.Customers.Add(c);
				db.SaveChanges();

				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
				return response;
			}
			else
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
			}
		}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}