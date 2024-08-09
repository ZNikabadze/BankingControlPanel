namespace BankingControlPanel.Shared.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
