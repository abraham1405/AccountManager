using AccountManager.Domain.Entities;
using AccountManager.Domain.Interfaces;
using AccountManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Infrastructure.Repositories
{
    public class AccountRepository(AccountDbContext accountDb) : IUserRepository
    {
        public async Task<Account?> GetUserAsync(string email)
        {
            var foundAccount = await accountDb.Accounts.FirstOrDefaultAsync(c => c.Email == email);
            return foundAccount;

        }

        public async Task<bool> AddAccountAsync(Account account)
        {
            bool exists = await accountDb.Accounts
            .AnyAsync(a => a.Email == account.Email);

            if (exists)
                return false;

            accountDb.Accounts.Add(account);
            await accountDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> GetUsernameAsync(string username)
        {
            return await accountDb.Accounts
                .AnyAsync(a => a.Username.ToLower() == username.ToLower());
        }
    }
}
