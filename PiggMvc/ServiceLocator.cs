using Pigg.CQRS;

namespace PiggMvc
{
    public static class ServiceLocator
    {
        public static IBus Bus { get; set; }
    }
}