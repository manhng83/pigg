using System;
using System.Collections.Generic;
using Pigg.CQRS;
using Pigg.CQRS.Events;
using Pigg.Model;

namespace PiggMvc3App.Models
{
    public class CustomPageListView 
    {
        public IEnumerable<CustomPage> CustomPages { get; set; }
    }
}