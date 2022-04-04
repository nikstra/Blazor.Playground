using System.Collections.Generic;

namespace WebUi.Models
{
    public class Week
    {
        public IEnumerable<Day> Days { get; set; }
        public int WeekNumber { get; set; }
    }
}