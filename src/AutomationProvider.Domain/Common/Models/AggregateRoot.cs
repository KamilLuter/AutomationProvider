namespace AutomationProvider.Domain.Common.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        protected AggregateRoot(TId id): base(id)
        {
        }

        #pragma warning disable CS8618
        protected AggregateRoot()
        {
        }
        #pragma warning restore CS8618

        protected void RaiseDomainEvent(IDomainEvent domainEvent) 
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
