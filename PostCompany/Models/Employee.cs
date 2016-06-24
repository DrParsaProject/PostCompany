using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PostCompany.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public EmployeeRole Role { get; set; }
	}
	public enum EmployeeRole
	{
		Manager,
		Counter,
		Transport,
		Weight
	}
}