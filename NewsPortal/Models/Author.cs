using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortal.Models
{
	
	public class Author : IUser
	{
		public string Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string UserName { get; set; }
		public int Rating { get; set; }
		public string MailId { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
		public virtual List<News> NewsID { get; set; }
	}
}