using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortal.ViewModels
{
	public class CommentViewModel
	{
		public long Id { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public long NewsId { get; set; }
		[Required]
		public long AuthorId { get; set; }
	}
}