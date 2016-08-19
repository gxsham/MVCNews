

using NewsPortal.Models;
using System.Collections.Generic;

namespace Repository.Interfaces
{
	public interface IRepository
	{
		void Save<TEntity>(TEntity entity) where TEntity : Entity;
		void Create<TEntity>(TEntity entity) where TEntity : Entity;
		void Delete<TEntity>(long id) where TEntity : Entity;
		IEnumerable<TEntity> GetAll<TEntity>() where TEntity : Entity;
		TEntity GetSingle<TEntity>(long id) where TEntity : Entity;

	}
}