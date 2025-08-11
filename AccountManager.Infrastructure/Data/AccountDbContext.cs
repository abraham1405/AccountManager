using AccountManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Infrastructure.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<ActiveSession> ActiveSessions { get; set; }

    }

}
