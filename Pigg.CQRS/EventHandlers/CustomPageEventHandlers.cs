using Pigg.CQRS.Events;
using System;

namespace Pigg.CQRS.EventHandlers
{
    public class CustomPageEventHandlers : IHandles<CustomPageCreated>, IHandles<CustomPageUpdated>,
                                         IHandles<CustomPageDeleted>
    {
        public void Handle(CustomPageDeleted message)
        {
            throw new NotImplementedException();
        }

        public void Handle(CustomPageCreated message)
        {
            throw new NotImplementedException();
        }

        public void Handle(CustomPageUpdated message)
        {
            throw new NotImplementedException();
        }
    }
}