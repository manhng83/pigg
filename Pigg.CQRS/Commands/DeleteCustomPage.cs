using System;

namespace Pigg.CQRS.Commands
{
    public class DeleteCustomPage : Command
    {
        public DeleteCustomPage(Guid entityId)
            : base(entityId)
        {
        }
    }
}