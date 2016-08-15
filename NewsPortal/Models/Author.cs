using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortal.Models
{
	public enum Role
	{
		Author, Admin
	}
	public class Author :Entity, IUser<long>
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string UserName { get; set; }
		public int Age { get; set; }
		public int Rating { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }	
		public virtual List<News> News { get; set; }

	}
}