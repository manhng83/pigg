using System;

namespace Pigg.CQRS
{
    public interface IMessage
    {
        Guid Id { get; }
    }
}