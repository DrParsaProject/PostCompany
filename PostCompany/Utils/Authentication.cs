using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Utils
{
	/// <summary>
	/// این کلاس توابعی را برای استفاده از سشن در اختیار می گذارد
	/// </summary>
	public class Authentication
	{
		/// <summary>
		/// تیم تابع سشن را برای کارمدی که به سیستم ورود کرده مقدار دهی می کند
		/// </summary>
		public static void AuthenticateEmployee(int id, EmployeeRole role)
		{
			System.Web.HttpContext.Current.Session["id"] = id;
			System.Web.HttpContext.Current.Session["role"] = role;
			System.Web.HttpContext.Current.Session["type"] = UserType.Employee;
		}

		/// <summary>
		/// این تابع سشن را برای مشتری ای که به سیستم ورود کرده مقدار دهی می کند
		/// </summary>
		public static void AuthenticateCustomer(int id)
		{
			System.Web.HttpContext.Current.Session["id"] = id;
			System.Web.HttpContext.Current.Session["role"] = null;
			System.Web.HttpContext.Current.Session["type"] = UserType.Customer;
		}

		/// <summary>
		/// این تابع شناسه کاربری فردی که در حال حاظر به سیستم ورود کرده را بر می گرداند
		/// </summary>
		public static int GetCurrnetUserId()
		{
			return (int) System.Web.HttpContext.Current.Session["id"];
		}

		/// <summary>
		/// این تابع نوع کاربری که در حال حاظر به سیستم ورود کرده را بر می گرداند
		/// </summary>
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

		/// <summary>
		/// این تابع نقش کاربری که در حال حاظر در سیستم ورود کرده را بر می گرداند
		/// </summary>
        public static EmployeeRole GetCurrnetUserRole()
        {
            return (EmployeeRole)System.Web.HttpContext.Current.Session["role"];
        }

        public static void LogOut()
        {
            System.Web.HttpContext.Current.Session.RemoveAll();
        }
	}

	/// <summary>
	/// انواع کاربران سیستم را مشخص می کند
	/// </summary>
	public enum UserType
	{
		Employee,
		Customer,
        UnAthorized
	}
}