
using System.Collections.Generic;
using Repository.Interfaces;
using System.Linq;
using Domain;
using System;

namespace Repository.Implementations
{
	public class NewsRepository : Repository, INewsRepository
	{

		public IEnumerable<News> GetByCategory(Category category)
		{ 
			return context.News.Where(x=>x.Category == category);
		}

		public News GetNewsWithComments(long newsId)
		{
			return null;
		}
	}
}
