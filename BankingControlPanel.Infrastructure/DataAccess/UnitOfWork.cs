using BankingControlPanel.Shared.Infrastructure;

namespace BankingControlPanel.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankingControlPanelDbContext _db;

        public UnitOfWork(BankingControlPanelDbContext db)
        {
            _db = db;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Handle domain events (Creating read models...)

                return await _db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
