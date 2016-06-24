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
    public class EmployeeController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

        // GET api/Employee
        public HttpResponseMessage GetEmployees()
		{
			if (!Authorize.hasRole(EmployeeRole.Manager))
				return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
			return Request.CreateResponse(HttpStatusCode.OK, db.Employees.AsEnumerable());
        }

        // GET api/Employee/5
        public HttpResponseMessage GetEmployee(int id)
		{
			if (!Authorize.hasRole(EmployeeRole.Manager))
				return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

			return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        // PUT api/Employee/5
        public HttpResponseMessage PutEmployee(int id, Employee employee)
		{
			if (!Authorize.hasRole(EmployeeRole.Manager))
				return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != employee.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(employee).State = EntityState.Modified;

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

        // POST api/Employee
        public HttpResponseMessage PostEmployee(RegisterEmployeeForm form)
        {
			if (!Authorize.hasRole(EmployeeRole.Manager))
				return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);

			Employee employee = new Employee();
			employee.Username = form.Username;
			employee.Password = Security.GetMd5Hash(form.Password);
			employee.Role = form.Role;

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, employee);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Employee/5
        public HttpResponseMessage DeleteEmployee(int id)
		{
			if (!Authorize.hasRole(EmployeeRole.Manager))
				return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Employees.Remove(employee);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}