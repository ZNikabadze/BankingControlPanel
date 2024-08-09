namespace BankingControlPanel.Shared.Infrastructure.Models
{
    public abstract class Entity
    {
        public DateTimeOffset ChangedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int Id { get; protected set; }
        public bool Deleted { get; set; }
    }
}
