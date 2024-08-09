namespace BankingControlPanel.Shared.Infrastructure.Models
{
    public abstract class DomainEvent
    {
        public Guid EventId { get; set; }
        public DateTimeOffset OccuredOn { get; set; }
        public string Type { get; set; }
    }
}
