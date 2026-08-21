using AzurePetMedicine.Common.Events;

namespace AzurePetMedicine.Common.Domains
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _changes;

        public int Version { get; private set; } = -1;

        protected AggregateRoot()
        {
            _changes = new List<IDomainEvent>();
        }

        public IEnumerable<IDomainEvent> GetChanges()
        {
            return _changes;
        }

        public void ClearChanges()
        {
            _changes.Clear();
        }
        
        public void ApplyDomainEvent(IDomainEvent domainEvent)
        {
            ChangeState(domainEvent);
            ValidateState();
            _changes.Add(domainEvent);
        }

        public void Load(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyDomainEvent(@event);
                Version++;
            }
            ClearChanges();
        }

        protected abstract void ChangeState(IDomainEvent domainEvent);
        protected abstract void ValidateState();
    }
}
