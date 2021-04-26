using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pigg.CQRS;

namespace PiggMvc3App
{
    public static class ServiceLocator
    {
        public static IBus Bus { get; set; }
    }
}