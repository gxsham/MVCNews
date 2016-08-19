

using NewsPortal.DAL;
using NewsPortal.Repository.Interfaces;
using System;

namespace NewsPortal.Repository.Implementations
{
	public class AuthorRepository : Repository, IAuthorRepository, IDisposable
	{
		private NewsContext newsContext;
		


		public AuthorRepository(NewsContext newsContext)
		{
			this.newsContext = newsContext;
		}

		
	}
}
