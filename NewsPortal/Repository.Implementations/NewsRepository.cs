

using NewsPortal.DAL;
using NewsPortal.Repository.Interfaces;

namespace NewsPortal.Repository.Implementations
{
	public class NewsRepository : Repository, INewsRepository
	{
		private NewsContext newsContext;

		public NewsRepository(NewsContext newsContext)
		{
			this.newsContext = newsContext;
		}
	}
}
