using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Reports
{
	public class EmployeeProfileReport
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public EmployeeRole Role { get; set; }

		public EmployeeProfileReport() { }
		public EmployeeProfileReport(Employee e)
		{
			Id = e.Id;
			Username = e.Username;
			Name = e.Name;
			Role = e.Role;
		}
	}
}