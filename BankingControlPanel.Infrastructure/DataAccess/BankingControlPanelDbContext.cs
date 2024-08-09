using BankingControlPanel.Domain.ClientManagement;
using BankingControlPanel.Domain.UserManagement;
using BankingControlPanel.Shared.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankingControlPanel.Infrastructure.DataAccess
{
    public class BankingControlPanelDbContext : DbContext
    {
        public BankingControlPanelDbContext(DbContextOptions<BankingControlPanelDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.Now;

            foreach (var item in ChangeTracker.Entries<Entity>().Where(entity => entity.State == EntityState.Added || entity.State == EntityState.Modified))
            {
                item.Entity.ChangedAt = now;
            }

            foreach (var item in ChangeTracker.Entries<Entity>().Where(entity => entity.State == EntityState.Added))
            {
                item.Entity.CreatedAt = now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
