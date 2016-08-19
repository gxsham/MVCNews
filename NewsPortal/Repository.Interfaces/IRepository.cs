



using NewsPortal.Models;
using System;
using System.Collections.Generic;

namespace NewsPortal.Repository.Interfaces
{
	public interface IRepository : IDisposable
	{
		void Save<TEntity>() where TEntity : Entity;
		void Create<TEntity>(TEntity entity) where TEntity : Entity;
		void Delete<TEntity>(long id) where TEntity : Entity;
		IList<TEntity> GetAll<TEntity>() where TEntity : Entity;
		TEntity GetSingle<TEntity>(long id) where TEntity : Entity;
		void Update<TEntity>(TEntity entity) where TEntity : Entity;

	}
}