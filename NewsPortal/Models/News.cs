
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Linq;
using System.Web;


namespace NewsPortal.Models
{
	public enum Category
	{
		Political, Social, Economic, Sport, IT, International, Curiosity
	}
	public class News : Entity
	{
		public string Topic { get; set; }
		public DateTime CreationDate { get; set; }
		public Category Category { get; set; }
		[AllowHtml]
		public string Text { get; set; }
		public int Rating { get; set; }
		public virtual Author Author { get; set; }
		[Required]
		public virtual long AuthorId { get; set; }
		public virtual string ImageLink { get; set; }
	}
}