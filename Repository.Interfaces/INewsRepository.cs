
using Domain;
using System.Collections.Generic;

namespace Repository.Interfaces
{
	public interface INewsRepository : IRepository
	{
		IEnumerable<News> GetByCategory(Category category);
	}
}
