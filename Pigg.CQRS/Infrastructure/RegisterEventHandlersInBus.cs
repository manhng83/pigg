using System;

namespace Pigg.CQRS.Infrastructure
{
    /// <summary>
    /// https://github.com/haf/Documently/blob/master/src/Documently.Infrastructure/RegisterEventHandlersInBus.cs
    /// </summary>
    public class RegisterEventHandlersInBus
    {
        public static void BootStrap(IMyContainer container)
        {
            new RegisterEventHandlersInBus().RegisterEventHandlers(container);
        }

        private void RegisterEventHandlers(IMyContainer container)
        {
            var bus = container.Resolve<IBus>();
            throw new NotImplementedException();
        }
    }

    public interface IMyContainer
    {
        void RegisterType<TInterface, TConcrete>();

        T Resolve<T>();
    }
}