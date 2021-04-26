using System;
using System.Collections.Generic;

namespace Pigg.CQRS
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion);

        List<DomainEvent> GetEventsForAggregate(Guid aggregateId);
    }
}