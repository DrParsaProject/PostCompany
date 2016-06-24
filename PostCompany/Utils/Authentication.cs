using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Utils
{
	public class Authentication
	{
		public static void Authenticate(int id, EmployeeRole role)
		{
			System.Web.HttpContext.Current.Session["id"] = id;
			System.Web.HttpContext.Current.Session["role"] = role;
			System.Web.HttpContext.Current.Session["type"] = UserType.Employee;
		}
		public static int GetCurrnetUserId()
		{
			return (int) System.Web.HttpContext.Current.Session["id"];
		}
	}

	public enum UserType
	{
		Employee,
		Customer
	}
}