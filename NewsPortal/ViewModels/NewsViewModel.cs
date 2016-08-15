using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NewsPortal.Models;
using System.Web.Mvc;

namespace NewsPortal.ViewModels
{
	public class NewsViewModel
	{
		public long Id { get; set; }
		[Required]
		public string Topic { get; set; }
		[Required]
		public Category Category { get; set; }
		[Display(Name ="Image Source")]
		public string ImageLink { get; set; }
		[Required]
		[AllowHtml]
		public string Text { get; set; }

	}
}