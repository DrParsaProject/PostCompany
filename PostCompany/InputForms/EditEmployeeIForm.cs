using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Forms
{
	/// <summary>
	/// این کلاس فرم ورودی برای تغییر مشخصات کارمند سسیستم است
	/// </summary>
	public class EditEmployeeIForm
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string Name { get; set; }
		public EmployeeRole Role { get; set; }
	}
}