﻿
using DAL;
using Domain;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Repository.Implementations
{
	public abstract class Repository : IRepository
	{
		protected readonly NewsContext context = new NewsContext();
		private bool disposed = false;

		public void Save<TEntity>() where TEntity : Entity
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

		public IList<TEntity> GetAll<TEntity>() where TEntity : Entity
		{
			return context.Set<TEntity>().ToList();
		}

		public TEntity GetSingle<TEntity>(long id) where TEntity : Entity
		{
			return context.Set<TEntity>().Find(id);
		}

		public void Update<TEntity>(TEntity entity) where TEntity : Entity
		{
			context.Entry(entity).State = EntityState.Modified;

			context.SaveChanges();
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

