using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.DAL;
using System.Security.Cryptography;
using System.Text;
using System.Linq;


namespace NewsPortal.Models
{
	public class UserStore : IUserStore<Author>, IUserPasswordStore<Author>, IUserEmailStore<Author>, IUserLockoutStore<Author, string>, IUserTwoFactorStore<Author, string>
	{ 
		NewsContext store = new NewsContext();
		public async Task CreateAsync(Author user)
		{
			store.Authors.Add(user);
			user.Id = new String(user.Password.Reverse().ToArray());
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

		public async Task<Author> FindByIdAsync(string userId) => await store.Authors.FindAsync(userId);

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
			user.MailId = email;
			await UpdateAsync(user);
		}

		public Task<string> GetEmailAsync(Author user)
		{
			return Task.FromResult(user.MailId);

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
			return await store.Authors.SingleOrDefaultAsync(x => x.MailId == email);
		}

		public Task<DateTimeOffset> GetLockoutEndDateAsync(Author user)
		{
			return Task.FromResult(new DateTimeOffset(2020, 10, 10, 5, 5, 6, TimeSpan.FromHours(1)));
		}

		// ni oceni code
		public Task SetLockoutEndDateAsync(Author user, DateTimeOffset lockoutEnd)
		{
			return store.SaveChangesAsync();
		}
		//ni oceni ocde
		public Task<int> IncrementAccessFailedCountAsync(Author user)
		{
			return store.SaveChangesAsync();
		}
		//ni oceni code
		public Task ResetAccessFailedCountAsync(Author user)
		{	
			return store.SaveChangesAsync();

		}
		// ni oceni code
		public Task<int> GetAccessFailedCountAsync(Author user) => Task.FromResult(0);

		public Task<bool> GetLockoutEnabledAsync(Author user)
		{
			//ni oceni implementate
			return Task.FromResult(false);
		}

		public Task SetLockoutEnabledAsync(Author user, bool enabled)
		{
			//ni oceni
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
	}



}