using System;

namespace Pigg.CQRS.Events
{
    public class CustomPageDeleted : DomainEvent
    {
        public Guid EntityId { get; set; }

        public CustomPageDeleted(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}