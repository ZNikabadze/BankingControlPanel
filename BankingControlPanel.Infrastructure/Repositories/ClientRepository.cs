using BankingControlPanel.Domain.ClientManagement;
using BankingControlPanel.Domain.ClientManagement.Respository;
using BankingControlPanel.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankingControlPanelDbContext _physicalPersonDbContext;

        public ClientRepository(BankingControlPanelDbContext physicalPersonDbContext)
        {
            _physicalPersonDbContext = physicalPersonDbContext;
        }

        public async Task<Client?> OfEmail(string email, CancellationToken cancellationToken)
        {
            return await _physicalPersonDbContext.Clients.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<Client?> OfId(int clientId, CancellationToken cancellationToken)
        {
            return await _physicalPersonDbContext.Clients.FindAsync([clientId], cancellationToken);
        }

        public async Task Store(Client client, CancellationToken cancellationToken)
        {
            await _physicalPersonDbContext.Clients.AddAsync(client, cancellationToken);
        }
    }
}
