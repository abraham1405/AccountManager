using AccountManager.Domain.Entities;

namespace AccountManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<Account?> GetUserAsync(string username);
        public Task<bool> AddAccountAsync(Account account);
        public Task<bool> GetUsernameAsync(string username);
    }
}
