using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Utils
{
	/// <summary>
	/// این کلاس توابعی را برای شناسایی کاربران در اختیار می گذارد
	/// </summary>
	public class Authorize
	{
		/// <summary>
		/// این تابع نقش کاربر فعلی را بررسی می کند
		/// </summary>
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

		/// <summary>
		/// این تابع بررسی می کند که کاربر مشخص شده چه نوعی دارد
		/// </summary>
		public static bool isCurrentUser(int id, UserType type)
		{
			return id == Authentication.GetCurrnetUserId() && 
				   type == Authentication.GetCurrnetUserType();
		}

		/// <summary>
		/// این تابع نوع کاربر فعلی را بررسی می کند
		/// </summary>
		public static bool isA(UserType type)
		{
			return Authentication.GetCurrnetUserType() == type;
		}
	}
}