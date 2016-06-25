using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.OutputForms
{
	public class CustomerProfileOForm
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }

		public CustomerProfileOForm() { }
		public CustomerProfileOForm(Customer c)
		{
			Id = c.Id;
			Username = c.Username;
			Name = c.Name;
			Phone = c.Phone;
			Address = c.Address;
		}
	}
}