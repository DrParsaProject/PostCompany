﻿using System;
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
using PostCompany.Reports;

namespace PostCompany.Controllers
{
    public class EmployeeController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

        // GET api/Employee
		public List<EmployeeProfileOForm> GetEmployees()
		{
			if (!Authorize.hasRole(EmployeeRole.Manager))
				throw new HttpResponseException(HttpStatusCode.Forbidden);

			List<Employee> list = db.Employees.ToList();
			List<EmployeeProfileOForm> res = new List<EmployeeProfileOForm>();
			foreach (Employee e in list)
				res.Add(new EmployeeProfileOForm(e));

			return res;
        }

        // GET api/Employee/5
        public EmployeeProfileOForm GetEmployee(int id)
		{
			if (!Authorize.hasRole(EmployeeRole.Manager) && !Authorize.isCurrentUser(id, UserType.Employee))
				throw new HttpResponseException(HttpStatusCode.Forbidden);
            
			Employee employee = db.Employees.Find(id);
            if (employee == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

			return new EmployeeProfileOForm(employee);
        }

        // PUT api/Employee/5
        public HttpResponseMessage PutEmployee(int id, EditEmployeeIForm form)
		{
			if (!Authorize.hasRole(EmployeeRole.Manager) && !Authorize.isCurrentUser(id, UserType.Employee))
				throw new HttpResponseException(HttpStatusCode.Forbidden);

			Employee emp = db.Employees.Find(id);
			if (emp == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			if (form.Name != null)
				emp.Name = form.Name;
			if (form.Role != EmployeeRole.Manager)
			{
				if (Authorize.hasRole(EmployeeRole.Manager))
					emp.Role = form.Role;
				else
					throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
			}
			if (form.OldPassword != null && form.NewPassword != null)
			{
				if (Security.VerifyMd5Hash(form.OldPassword, emp.Password))
					emp.Password = Security.GetMd5Hash(form.NewPassword);
				else
					throw new HttpResponseException(HttpStatusCode.NotAcceptable);
			}

			db.Entry(emp).State = EntityState.Modified;

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
        public HttpResponseMessage PostEmployee(RegisterEmployeeIForm form)
        {
            if (!Authorize.hasRole(EmployeeRole.Manager))
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            if (form.Role == EmployeeRole.Manager)
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

			Employee employee = new Employee();
			employee.Username = form.Username;
			employee.Password = Security.GetMd5Hash(form.Password);
			employee.Name = form.Name;
			employee.Role = form.Role;

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
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
				throw new HttpResponseException(HttpStatusCode.Forbidden);

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
				throw new HttpResponseException(HttpStatusCode.NotFound);
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