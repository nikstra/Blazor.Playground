using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class CalendarEntry
    {
        public int Id { get; }
        public string Name { get; set; }
        public DateTimeOffset Start { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset End => Start.Add(Duration);
    }
}