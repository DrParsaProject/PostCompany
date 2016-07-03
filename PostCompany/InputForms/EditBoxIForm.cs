using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.InputForms
{
	/// <summary>
	/// این کلاس فرم ورودی برای تغییر مشخصات یک بسته پستی است
	/// </summary>
	public class EditBoxIForm
	{
		public string ReceiverName { get; set; }
		public string ReceiverCity { get; set; }
		public string ReceiverAddress { get; set; }
		public double Weight { get; set; }
		public PostStatus Status { get; set; }
	}
}