using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NewsPortal.Models;

namespace NewsPortal.DAL
{
	public class NewsInitializer : CreateDatabaseIfNotExists<NewsContext>
	{
		protected override void Seed(NewsContext context)
		{
			var news = new List<News>
			{
				new News {Topic = "LOL", Category = Category.IT, CreationDate = DateTime.Now, Rating = 10, Text = "treeedfnvsdfvsjbvfsb"   },
				new News {Topic = "LOL", Category = Category.IT, CreationDate = DateTime.Now, Rating = 10, Text = "treeedfnvsdfvsjbvfsb"   }
			};

			Author author = new Author { FirstName = "Dorin", LastName ="Balan",MailId = @"gxsham@gmail.com", Role= "Admin", Password = @"100De_100", NewsID = news, Rating = 50 };
			context.Authors.Add(author);
			context.SaveChanges();
		}
	}
}