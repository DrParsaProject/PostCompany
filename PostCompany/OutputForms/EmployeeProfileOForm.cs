using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Reports
{
	/// <summary>
	/// این کلاس فرم خروجی برای مشخصات کارمند است
	/// </summary>
	public class EmployeeProfileOForm
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public EmployeeRole Role { get; set; }

		public EmployeeProfileOForm() { }
		public EmployeeProfileOForm(Employee e)
		{
			Id = e.Id;
			Username = e.Username;
			Name = e.Name;
			Role = e.Role;
		}
	}
}