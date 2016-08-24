

using Domain;
using System.Data.Entity;

namespace DAL
{
	public class NewsContext : DbContext
	{
		public NewsContext() : base("NewsContext")
		{

		}

		public DbSet<Author> Authors { get; set; }
		public DbSet<News> News { get; set; }
		public DbSet<Comment> Comment { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<News>().Property(x => x.Topic).IsRequired();
			modelBuilder.Entity<News>().Property(x => x.Text).IsRequired();
			modelBuilder.Entity<Author>().Property(x => x.FirstName).IsRequired();
			modelBuilder.Entity<Author>().Property(x => x.LastName).IsRequired();
			modelBuilder.Entity<Author>().Property(x => x.UserName).IsRequired();
			modelBuilder.Entity<Author>().Property(x => x.Age).IsRequired();
			modelBuilder.Entity<Author>().Property(x => x.Password).IsRequired();
			modelBuilder.Entity<News>().HasMany(x => x.Comment).WithRequired(x => x.News).WillCascadeOnDelete(true);
			modelBuilder.Entity<Comment>().HasOptional(x => x.Author).WithOptionalDependent().WillCascadeOnDelete(false);
		}


	}
}
