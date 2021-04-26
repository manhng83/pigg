namespace Pigg.CQRS
{
    public interface IHandles<T>
    {
        void Handle(T message);
    }
}