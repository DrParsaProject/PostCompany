using PostCompany.Models;
using PostCompany.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.OutputForms
{
	/// <summary>
	/// این کلاس فرم خروجی برای ورود به سیستم کاربران است
	/// شناسه کاربری و نوع کاربر و نقش او در سیستم را مشخص میکند
	/// </summary>
    public class LoginOForm
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public EmployeeRole Role { get; set; }
    }
}