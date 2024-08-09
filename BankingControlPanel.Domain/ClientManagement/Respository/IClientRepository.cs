namespace BankingControlPanel.Domain.ClientManagement.Respository
{
    public interface IClientRepository
    {
        Task Store(Client client, CancellationToken cancellationToken);
        Task<Client?> OfId(int clientId, CancellationToken cancellationToken);
        Task<Client?> OfEmail(string email, CancellationToken cancellationToken);
    }
}
