﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }

	}
}