using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PostCompany.Models
{
	/// <summary>
	/// این کلاس مدل دیتابیس برای کارمند است
	/// </summary>
	public class Employee
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public EmployeeRole Role { get; set; }
	}

	/// <summary>
	/// نقش یک کارمند در سیستم را مشخص می کند
	/// </summary>
	public enum EmployeeRole
	{
		Manager,
		Counter,
		Transport,
		Weight
	}
}