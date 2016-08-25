
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Domain
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
		public virtual ICollection<Comment> Comment { get; set; }
		public virtual ICollection<Like> Like { get; set; }

	}
}