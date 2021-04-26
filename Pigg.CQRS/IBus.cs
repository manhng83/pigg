using System;

namespace Pigg.CQRS
{
    public interface IBus : ICommandSender, IEventPublisher
    {
        void RegisterHandler<T>(Action<T> handler) where T : IMessage;
    }
}