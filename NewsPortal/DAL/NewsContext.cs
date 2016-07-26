using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsPortal.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;

namespace NewsPortal.DAL
{
	public class NewsContext : DbContext
	{
		public NewsContext() : base("NewsContext")
		{

		}

		public DbSet<Author> Authors { get; set; }
		public DbSet<News> News { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<News>().HasKey(x => x.Id);
			modelBuilder.Entity<Author>().HasKey(x => x.Id);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

	}
}