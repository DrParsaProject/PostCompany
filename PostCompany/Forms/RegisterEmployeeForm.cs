using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Forms
{
	public class RegisterEmployeeForm
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public EmployeeRole Role { get; set; }
	}
}