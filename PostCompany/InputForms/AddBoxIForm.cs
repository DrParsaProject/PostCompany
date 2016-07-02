using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.InputForms
{
	public class AddBoxIForm
	{
		public int SenderId { get; set; }
		public string ReceiverName { get; set; }
		public string ReceiverCity { get; set; }
		public string ReceiverAddress { get; set; }
	}
}