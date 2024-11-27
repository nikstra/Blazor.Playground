using System.Collections.Generic;

namespace WebUi.Components.Calendar.Models
{
    public class Week
    {
        public IEnumerable<Day> Days { get; set; } = Enumerable.Empty<Day>();
        public int WeekNumber { get; set; }
    }
}