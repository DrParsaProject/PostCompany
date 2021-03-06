﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostCompany.Models
{
	public class Box
	{
		public int Id { get; set; }

		[ForeignKey("Sender")]
		public int SenderId { get; set; }
		public virtual Customer Sender { get; set; }

		public Nullable<DateTime> AddedOn { get; set; }
		public Nullable<DateTime> SentOn { get; set; }
		public Nullable<DateTime> ReceivedOn { get; set; }

		public string ReceiverName { get; set; }
		public string ReceiverCity { get; set; }
		public string ReceiverAddress { get; set; }

		public double Weight { get; set; }
		public double Cost { get; set; }
		public PostStatus Status { get; set; }
	}

	public enum PostStatus
	{
		NotPaid,
		Pending,
		Sending,
		Received,
		Canceled,
		Failed
	}
}