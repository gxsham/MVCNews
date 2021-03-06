﻿using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using Domain;

namespace DAL
{


	public class UserStore : IUserStore<Author, long>, IUserPasswordStore<Author, long>, IUserEmailStore<Author, long>, IUserLockoutStore<Author, long>, IUserTwoFactorStore<Author, long>, IUserRoleStore<Author, long>
	{
		NewsContext store = new NewsContext();
		public async Task CreateAsync(Author user)
		{
			store.Authors.Add(user);
			await store.SaveChangesAsync();
		}

		public async Task DeleteAsync(Author user)
		{
			store.Authors.Remove(user);
			await store.SaveChangesAsync();
		}

		public void Dispose()
		{
			store.Dispose();
		}

		public async Task<Author> FindByIdAsync(long userId) => await store.Authors.FindAsync(userId);

		public async Task<Author> FindByNameAsync(string userName) => await store.Authors.SingleOrDefaultAsync(x => x.UserName == userName);

		public async Task<string> GetPasswordHashAsync(Author user)
		{
			return await Task.FromResult(user.Password);
		}

		public Task<bool> HasPasswordAsync(Author user)
		{
			return Task.FromResult(true);
		}

		public Task SetPasswordHashAsync(Author user, string passwordHash)
		{
			user.Password = passwordHash;
			return Task.FromResult<object>(null);
		}

		public async Task UpdateAsync(Author user)
		{
			var entity = await store.Authors.FindAsync(user.Id);
			var entry = store.Entry(entity);
			entry.State = EntityState.Modified;
			await store.SaveChangesAsync();
		}

		private Task<string> HashAsync(string password) => Task.Run(() => Hash(password));

		private string Hash(string password)
		{
			SHA256 mySHA256 = SHA256Managed.Create();
			byte[] hashValue = mySHA256.ComputeHash(Convert.FromBase64String(password));
			return Convert.ToBase64String(hashValue);
		}

		public async Task SetEmailAsync(Author user, string email)
		{
			user.UserName = email;
			await UpdateAsync(user);
		}

		public Task<string> GetEmailAsync(Author user)
		{
			return Task.FromResult(user.UserName);

		}

		public Task<bool> GetEmailConfirmedAsync(Author user)
		{
			return Task.FromResult(true);
		}

		public Task SetEmailConfirmedAsync(Author user, bool confirmed)
		{
			throw new NotImplementedException();
		}

		public async Task<Author> FindByEmailAsync(string email)
		{
			return await store.Authors.SingleOrDefaultAsync(x => x.UserName == email);
		}

		public Task<DateTimeOffset> GetLockoutEndDateAsync(Author user)
		{
			return Task.FromResult(new DateTimeOffset(2020, 10, 10, 5, 5, 6, TimeSpan.FromHours(1)));
		}

		// not needed 
		public Task SetLockoutEndDateAsync(Author user, DateTimeOffset lockoutEnd)
		{
			return store.SaveChangesAsync();
		}
		//not needed
		public Task<int> IncrementAccessFailedCountAsync(Author user)
		{
			return store.SaveChangesAsync();
		}
		//not needed
		public Task ResetAccessFailedCountAsync(Author user)
		{
			return store.SaveChangesAsync();

		}
		// not needed
		public Task<int> GetAccessFailedCountAsync(Author user) => Task.FromResult(0);

		public Task<bool> GetLockoutEnabledAsync(Author user)
		{
			//not needed
			return Task.FromResult(false);
		}

		public Task SetLockoutEnabledAsync(Author user, bool enabled)
		{
			//not needed
			return Task.FromResult(false);
		}

		public Task SetTwoFactorEnabledAsync(Author user, bool enabled)
		{
			return Task.FromResult(false);
		}

		public Task<bool> GetTwoFactorEnabledAsync(Author user)
		{
			return Task.FromResult(false);
		}

		//user role store
		public Task AddToRoleAsync(Author user, string roleName)
		{
			throw new NotImplementedException();
		}

		public Task RemoveFromRoleAsync(Author user, string roleName)
		{
			throw new NotImplementedException();
		}

		public Task<IList<string>> GetRolesAsync(Author user)
		{
			IList<string> list = new List<string>();
			list.Add(user.Role.ToString());
			return Task.FromResult(list);
		}

		public Task<bool> IsInRoleAsync(Author user, string roleName)
		{
			if (ReferenceEquals(user, null))
				throw new ArgumentException("user is null", nameof(user));
			if (ReferenceEquals(roleName, null))
				throw new ArgumentException("role is null", nameof(roleName));
			return Task.FromResult(user.Role.ToString() == roleName);
		}
	}


}