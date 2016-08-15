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
		}
	}
}