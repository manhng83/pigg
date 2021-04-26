namespace Pigg.CQRS
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : DomainEvent;
    }
}