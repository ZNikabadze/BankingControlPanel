namespace BankingControlPanel.Shared.Infrastructure.Models
{
    public abstract class AggregateRoot<TIdentity> : Entity where TIdentity : struct
    {
        protected AggregateRoot()
        {

        }

        public int Version { get; protected set; }
        protected IList<DomainEvent> Events { get; } = new List<DomainEvent>();

        protected void Raise(DomainEvent @event)
        {
            @event.EventId = Guid.NewGuid();
            @event.OccuredOn = DateTimeOffset.Now;
            @event.Type = @event.GetType().Name;

            if (Version == default)
            {
                CreatedAt = DateTimeOffset.Now;
            }

            Version++;
            ChangedAt = DateTimeOffset.Now;
            Events.Add(@event);
        }
    }
}
