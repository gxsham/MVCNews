
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;
using System;
using System.Collections.Generic;

namespace NewsPortal.ViewModels
{
	public class CreateNewsViewModel
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

	public class DeleteNewsViewModel
	{
		public long Id { get; set; }
		public string Topic { get; set; }
		public DateTime CreationTime { get; set; }
		public Category Category { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
	}

	public class DetailsNewsViewModel
	{
		public long Id { get; set; }
		public string Topic { get; set; }
		public DateTime CreationDate { get; set; }
		public Category Category { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
		public string AuthorUserName { get; set; }
		public string ImageLink { get; set; }
	}

	public class EditNewsViewModel
	{
		public long Id { get; set; }
		[Required]
		public string Topic { get; set; }
		[Required]
		public Category Category { get; set; }
		[Required]
		[AllowHtml]
		public string Text { get; set; }
		public string ImageLink { get; set; }
	}

	public class FullNewsViewModel
	{
		public long Id { get; set;}
		public string Topic { get; set; }
		public Category Category { get; set; }
		public DateTime CreationDate { get; set; }
		public string AuthorUserName { get; set; }
		public long AuthorId { get; set; }
		public string ImageLink { get; set; }
		public string Text { get; set;}
		public int Rating { get; set; }
		public int CommentCount { get; set; }
		public List<Comment> Comments { get; set; }
	}


	public class AdminIndexNewsViewModel
	{
		public long Id { get; set; }
		public string Topic { get; set; }
		public DateTime CreationTime { get; set; }
		public Category Category { get; set; }
		public int Rating { get; set; }
		public long AuthorId { get; set; }
	}

	public class PublicIndexNewsViewModel
	{
		public long Id { get; set; }
		public string Topic { get; set; }
		public DateTime CreationDate { get; set; }
		public Category Category { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
		public long AuthorId { get; set; }
		public string AuthorUserName { get; set; }
		public string ImageLink { get; set; }
	}
}