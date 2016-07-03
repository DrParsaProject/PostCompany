using PostCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostCompany.OutputForms
{
	/// <summary>
	/// این کلاس فرم خروجی برای بسته پستی است
	/// </summary>
	public class BoxOForm
	{
		public int Id { get; set; }
		public CustomerProfileOForm Sender { get; set; }
		public ReceiverForm Receiver { get; set; }

		public Nullable<DateTime> AddedOn { get; set; }
		public Nullable<DateTime> SentOn { get; set; }
		public Nullable<DateTime> ReceivedOn { get; set; }

		public double Weight { get; set; }
		public double Cost { get; set; }
		public PostStatus Status { get; set; }

		public BoxOForm() { }

		public BoxOForm(Box b)
		{
			Id = b.Id;
			AddedOn = b.AddedOn;
			SentOn = b.SentOn;
			ReceivedOn = b.ReceivedOn;

			Receiver = new ReceiverForm();
			Sender = new CustomerProfileOForm(b.Sender);

			Weight = b.Weight;
			Cost = b.Cost;
			Status = b.Status;
		}
	}

	public class ReceiverForm
	{
		public string Name { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
	}
}