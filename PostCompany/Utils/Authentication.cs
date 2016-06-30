using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Utils
{
	public class Authentication
	{
		public static void AuthenticateEmployee(int id, EmployeeRole role)
		{
			System.Web.HttpContext.Current.Session["id"] = id;
			System.Web.HttpContext.Current.Session["role"] = role;
			System.Web.HttpContext.Current.Session["type"] = UserType.Employee;
		}
		public static void AuthenticateCustomer(int id)
		{
			System.Web.HttpContext.Current.Session["id"] = id;
			System.Web.HttpContext.Current.Session["role"] = null;
			System.Web.HttpContext.Current.Session["type"] = UserType.Customer;
		}
		public static int GetCurrnetUserId()
		{
			return (int) System.Web.HttpContext.Current.Session["id"];
		}
		public static UserType GetCurrnetUserType()
		{
            try
            {
                return (UserType) System.Web.HttpContext.Current.Session["type"];
            }
            catch
            {
                return UserType.UnAthorized;
            }
		}

        public static EmployeeRole GetCurrnetUserRole()
        {
            return (EmployeeRole)System.Web.HttpContext.Current.Session["role"];
        }

        public static void LogOut()
        {
            System.Web.HttpContext.Current.Session.RemoveAll();
        }
	}

	public enum UserType
	{
		Employee,
		Customer,
        UnAthorized
	}
}