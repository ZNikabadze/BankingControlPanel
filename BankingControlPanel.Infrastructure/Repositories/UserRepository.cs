using BankingControlPanel.Domain.UserManagement;
using BankingControlPanel.Domain.UserManagement.Repository;
using BankingControlPanel.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly BankingControlPanelDbContext _physicalPersonDbContext;

        public UserRepository(BankingControlPanelDbContext physicalPersonDbContext)
        {
            _physicalPersonDbContext = physicalPersonDbContext;
        }

        public async Task<User?> OfId(int userId, CancellationToken cancellationToken)
        {
            return await _physicalPersonDbContext.Users.FindAsync([userId], cancellationToken);
        }

        public async Task<User?> OfName(string name, CancellationToken cancellationToken)
        {
            return await _physicalPersonDbContext.Users.SingleOrDefaultAsync(x => x.UserName == name, cancellationToken);
        }

        public async Task Store(User user, CancellationToken cancellationToken)
        {
            await _physicalPersonDbContext.Users.AddAsync(user, cancellationToken);
        }
    }
}
