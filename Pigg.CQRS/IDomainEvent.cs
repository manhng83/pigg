using System;

namespace Pigg.CQRS
{
    public interface IDomainEvent : IMessage
    {
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}