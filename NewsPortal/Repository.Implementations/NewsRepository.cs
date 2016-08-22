

using System;
using System.Collections.Generic;
using NewsPortal.DAL;
using NewsPortal.Models;
using NewsPortal.Repository.Interfaces;
using System.Linq;

namespace NewsPortal.Repository.Implementations
{
	public class NewsRepository : Repository, INewsRepository
	{

		public IEnumerable<News> GetByCategory(Category category)
		{ 
			return context.News.Where(x=>x.Category == category);
		}
	}
}
