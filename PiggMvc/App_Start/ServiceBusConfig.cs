using Pigg.CQRS;
using Pigg.CQRS.CommandHandlers;
using Pigg.CQRS.Commands;
using Pigg.CQRS.Domain;
using Pigg.CQRS.EventHandlers;
using Pigg.CQRS.Events;
using Pigg.Data;
using Pigg.Data.Repositories;

namespace PiggMvc
{
    public class ServiceBusConfig
    {
        public static void Run()
        {
            IBus bus = new Bus();
            var storage = new EventStore(bus);
            var repo = new Repository<CustomPage>(storage);
            var commands = new CustomPageCommandHandlers(new CustomPageRepository(new DatabaseFactory()), repo);
            bus.RegisterHandler<CreateCustomPage>(commands.Handle);
            bus.RegisterHandler<UpdateCustomPage>(commands.Handle);
            bus.RegisterHandler<DeleteCustomPage>(commands.Handle);

            var customPageReporting = new CustomPageEventHandlers();

            bus.RegisterHandler<CustomPageCreated>(customPageReporting.Handle);
            bus.RegisterHandler<CustomPageDeleted>(customPageReporting.Handle);
            bus.RegisterHandler<CustomPageUpdated>(customPageReporting.Handle);

            ServiceLocator.Bus = bus;
        }
    }
}