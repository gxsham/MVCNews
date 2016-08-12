using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPortal.Models
{
	public class Role : IRole
	{
		public string Id { get;  }
		
		public string Name { get; set; }
		
	}
}