using AccountManager.Domain.Entities;
using AccountManager.Domain.Interfaces;
using AccountManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace AccountManager.Infrastructure.Repositories
{
    public class SessionRepository(AccountDbContext accountDb) : ISessionRepository
    {
        public async Task AddSession(Account account, string token)
        {
            ActiveSession session = new ActiveSession();
            session.AccountId = account.Id;
            session.Token = token;
            session.ExpiresAt = DateTime.UtcNow.AddMinutes(5);

            await accountDb.ActiveSessions.AddAsync(session);
            await accountDb.SaveChangesAsync();
        }
        public async Task DeletedSessionAsync(ActiveSession session)
        {
            accountDb.ActiveSessions.Remove(session);
            await accountDb.SaveChangesAsync();
        }

        public async Task<ActiveSession?> GetSessionActiveASync(string token)
        {
            return await accountDb.ActiveSessions.FirstOrDefaultAsync(s => s.Token == token && s.ExpiresAt > DateTime.UtcNow);
        }
    }
}
