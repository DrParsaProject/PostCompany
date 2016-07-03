using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.InputForms
{
	/// <summary>
	/// این کلاس فرم ورودی برای ثبت نام یک مشتری جدید در سیستم است
	/// </summary>
	public class RegisterCustomerIForm
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
	}
}