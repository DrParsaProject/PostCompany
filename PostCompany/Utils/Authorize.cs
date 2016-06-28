using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Utils
{
	public class Authorize
	{
		public static bool hasRole(EmployeeRole role)
		{
			if (System.Web.HttpContext.Current.Session["role"] == null)
				return false;
			if ((UserType) System.Web.HttpContext.Current.Session["type"] != UserType.Employee)
				return false;
			if ((EmployeeRole) System.Web.HttpContext.Current.Session["role"] != role)
				return false;
			return true;
		}

		public static bool isCurrentUser(int id, UserType type)
		{
			return id == Authentication.GetCurrnetUserId() && 
				   type == Authentication.GetCurrnetUserType();
		}

		public static bool isA(UserType type)
		{
			return Authentication.GetCurrnetUserType() == type;
		}
	}
}