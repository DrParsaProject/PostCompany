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
using PostCompany.OutputForms;

namespace PostCompany.Controllers
{
	/// <summary>
	/// این کلاس وظیفه ورود به سیستم برای کارمندان شرکت را بر عهده دارد
	/// </summary>
    public class EmployeeLoginController : ApiController
    {
        private PostCompanyContext db = new PostCompanyContext();

		/// <summary>
		/// این تابع با گرفتن یوزرنیم و پسورد کارمند او را به سیستم وارد می کند
		/// </summary>
        // POST api/EmployeeLogin
        public LoginOForm PostEmployeeLogin(LoginIForm form)
        {
            form.Password = Security.GetMd5Hash(form.Password);

            var user = (from e in db.Employees
                        where e.Username == form.Username &&
                              e.Password == form.Password
                        select new { e.Id, e.Role }).SingleOrDefault();

            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            Authentication.AuthenticateEmployee(user.Id, user.Role);
            LoginOForm res = new LoginOForm();
            res.Id = user.Id;
            res.Role = user.Role;
            res.Type = UserType.Employee;
            return res;
        }
    }
}