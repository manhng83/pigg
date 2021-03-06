using System;

namespace Pigg.CQRS
{
    [Serializable]
    public class DomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}