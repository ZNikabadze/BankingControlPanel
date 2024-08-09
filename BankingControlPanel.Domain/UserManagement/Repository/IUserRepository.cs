namespace BankingControlPanel.Domain.UserManagement.Repository
{
    public interface IUserRepository
    {
        Task Store(User user, CancellationToken cancellationToken);
        Task<User?> OfId(int userId, CancellationToken cancellationToken);
    }
}
