using AccountManager.Domain.Entities;
using System.ComponentModel.Design;

namespace AccountManager.Domain.Interfaces
{
    public interface ISessionRepository
    {
        public Task AddSession(Account account, string token);
        public Task<ActiveSession?> GetSessionActiveASync(string token);
        public Task DeletedSessionAsync(ActiveSession session);
    }
}
