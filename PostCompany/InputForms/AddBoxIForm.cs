using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.InputForms
{
	/// <summary>
	/// این کلاس فرم ورودی برای ریکوست ساهتن بسته پستی است
	/// </summary>
	public class AddBoxIForm
	{
		public string SenderUsername { get; set; }
		public string ReceiverName { get; set; }
		public string ReceiverCity { get; set; }
		public string ReceiverAddress { get; set; }
	}
}