using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.InputForms
{
	/// <summary>
	/// این کلاس فرم ورودی برای تغییر مشخصات مشتری سیستم است
	/// </summary>
	public class EditCustomerIForm
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
	}
}