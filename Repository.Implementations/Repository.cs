
using NewsPortal.DAL;
using NewsPortal.Models;
using Repository.Interfaces;
using System;
using System.Activities.Debugger;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
	public abstract class Repository : IRepository, IDisposable
	{
		protected readonly NewsContext context = new NewsContext();
		private bool disposed = false;

		public void Save<TEntity>(TEntity entity) where TEntity : Entity
		{
			context.SaveChanges();
		}

		public void Create<TEntity>(TEntity entity) where TEntity : Entity
		{
			context.Set<TEntity>().Add(entity);
			context.SaveChanges();
		}

		public void Delete<TEntity>(long id) where TEntity : Entity
		{
			TEntity entity = context.Set<TEntity>().Find(id);
			context.Set<TEntity>().Remove(entity);
			context.SaveChanges();
		}

		public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : Entity
		{
			return context.Set<TEntity>().ToList();
		}

		public TEntity GetSingle<TEntity>(long id) where TEntity : Entity
		{
			return context.Set<TEntity>().Find(id);
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


	}


}

