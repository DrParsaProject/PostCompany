using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Forms
{
	/// <summary>
	/// این کلاس فرم ورودی برای اضافه کردن کارمند جدید به سیستم است
	/// </summary>
	public class RegisterEmployeeIForm
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public EmployeeRole Role { get; set; }
	}
}